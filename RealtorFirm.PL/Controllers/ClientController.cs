using AutoMapper;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Interfaces;
using RealtorFirm.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RealtorFirm.PL.Controllers
{
    public class ClientController : Controller
    {
        IAppartmentService appartmentService;
        IUserService userService;
        IClientService clientService;
        IApplyService applyService;
        public ClientController(IAppartmentService appartmentService,
                                    IUserService userService,
                                    IClientService clientService,
                                    IApplyService applyService)
        {
            this.appartmentService = appartmentService;
            this.userService = userService;
            this.clientService = clientService;
            this.applyService = applyService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (Session["userId"] != null)
            {
                ViewBag.Id = Session["userId"];
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpPost]
        public ActionResult Add(ClientDTO clientDto)
        {
            if (Session["userId"] != null)
            {
               
                int id = int.Parse(Session["UserId"].ToString());
              
                    clientService.Add(clientDto);
                    return RedirectToAction("Page", "User");
                
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpGet]
        public ActionResult AddExistClient()
        {
            if (Session["userId"] != null)
            {
                ViewBag.Id = Session["userId"];
                int userId = 0;
                IEnumerable<ClientDTO> clientDTOs = clientService.GetUser(userId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientDTOs);
                return View(client);
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpPost]
        public ActionResult AddExistClient(ClientDTO clientDTO)
        {
            if (Session["userId"] != null)
            {
                int id = int.Parse(Session["UserId"].ToString());
                clientDTO.UserId = id;
                clientService.Add(clientDTO);
                return RedirectToAction("Page", "User");
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Deleted(int id2)
        {
            if (Session["userId"] != null)
            {
                clientService.Delete(id2);
                return RedirectToAction("Page", "User");
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            if (Session["userId"] != null)
            {
                ClientDTO clientDtos = clientService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<ClientDTO, ClientModel>(clientDtos);

                ViewBag.Client = client;
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpPost]
        public ActionResult Update(ClientDTO client)
        {
            if (Session["userId"] != null)
            {
                int clientId = client.ClientId;
                clientService.Update(client);
                ClientDTO clientDto = clientService.Get(client.Name, client.Surname, client.DateOfBirth);
                int newId = clientDto.ClientId;

                IEnumerable<AppartmentDTO> appDtos = appartmentService.GetForClient(clientId);
                foreach (var n in appDtos)
                {
                    appartmentService.UpdateForClient(n,newId);
                }
                return RedirectToAction("Page", "User");
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Contacts(int id)
        {
            if (Session["userId"] != null)
            {
                ClientDTO clientDTOs = clientService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<ClientDTO, ClientModel>(clientDTOs);

                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetForClient(id);
                var appmapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = appmapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appartmentDTOs);

                IEnumerable<ApplyDTO> applyDTOs = applyService.GetClient(id);
                var applymapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var applies = applymapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);

                ViewBag.Client = client;
                ViewBag.Apply = applies;
                
                return View(appartments);
            }
            else
                return RedirectToAction("Enter", "User");

        }

        public ActionResult Renter(int id)
        {
            if (Session["userId"] != null)
            {
                ClientDTO clientDTOs = clientService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<ClientDTO, ClientModel>(clientDTOs);

                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetForClient(id);
                var appartmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = appartmentMapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appartmentDTOs);

                IEnumerable<ApplyDTO> applyDTOs = applyService.GetClient(id);
                var applyMapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var applies = applyMapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);

                ViewBag.Client = client;
                ViewBag.Apply = applies;

                return View(appartments);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult All(string sort, string keyword)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<ClientDTO> clientDTOs = clientService.GetAll();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var clients = mapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientDTOs);
                var sortClients = from c in clients
                                  select c;
                switch (sort)
                {
                    case "name":
                        sortClients = sortClients.OrderBy(s => s.Name);
                        break;

                    case "without":
                        sortClients = sortClients.OrderBy(s => s.ClientId);
                        break;

                    case "surname":
                        sortClients = sortClients.OrderBy(s => s.Surname);
                        break;

                    case "first_number":
                        sortClients = sortClients.OrderBy(s => s.Account.Substring(0,1));
                        break;

                    case "keyword":
                        if(!ReferenceEquals(keyword,null))
                        sortClients = sortClients.Where(
                            c => c.Surname.ToLower().StartsWith(keyword.ToLower()) || c.Name.ToLower().StartsWith(keyword.ToLower())
                            || c.Patronimic.ToLower().StartsWith(keyword.ToLower())); ;
                        break;
                       
                    default:
                        sortClients = sortClients.OrderBy(s => s.ClientId);
                        break;
                }

                return View(sortClients);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Details(int id)
        {
            if (Session["userId"] != null)
            {

                ClientDTO clientDTO = clientService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<ClientDTO, ClientModel>(clientDTO);

                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetForClient(id);
                var appartmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = appartmentMapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appartmentDTOs);

                int? userId = clientDTO.UserId;
                UserDTO userDTO = userService.Get(userId);
                var userMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                var user = userMapper.Map<UserDTO, UserModel>(userDTO);

                ViewBag.Client = client;
                ViewBag.User = user;

                return View(appartments);
            }
            else
                return RedirectToAction("Enter", "User");

        }

        public ActionResult Search(string sort, string keyword, string keyword1)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetAll();
                var appartmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = appartmentMapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appartmentDTOs);

                IEnumerable<ClientDTO> clientDTOs = clientService.GetAll();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var clients = mapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientDTOs);

                var sortClients = from c in clients
                                  select c;

                var sortAppartments = from a in appartments
                               select a;
                var model = new ClientsAndAppartmentsModel();

                if (!ReferenceEquals(keyword, null))
                {
                    switch (sort)
                    {
                        case "search":
                            sortClients = sortClients.Where(
                                c => c.Surname.ToLower().StartsWith(keyword.ToLower()) || c.Name.ToLower().StartsWith(keyword.ToLower())
                                || c.Patronimic.ToLower().StartsWith(keyword.ToLower()));

                            sortAppartments = sortAppartments.Where(
                                           a => a.Address.ToLower().StartsWith(keyword.ToLower()) || a.City.ToLower().StartsWith(keyword.ToLower())
                                           || a.Price.ToString().StartsWith(keyword));
                            model.ClientsModel = sortClients;
                            model.AppartmentsModel = sortAppartments;
                            break;

                        case "deep_search":
                            sortClients = sortClients.Where(
                            c => c.Surname.ToLower().StartsWith(keyword.ToLower()) || c.Name.ToLower().StartsWith(keyword.ToLower())
                            || c.Patronimic.ToLower().StartsWith(keyword.ToLower()));

                            sortAppartments = sortAppartments.Where(
                                           a => a.Address.ToLower().StartsWith(keyword1.ToLower()) || a.City.ToLower().StartsWith(keyword1.ToLower())
                                           || a.Price.ToString().StartsWith(keyword1));
                            model.ClientsModel = sortClients;
                            model.AppartmentsModel = sortAppartments;
                            break;

                        default:
                            sortClients = sortClients.OrderBy(c => c.ClientId);
                            sortAppartments = sortAppartments.OrderBy(a => a.AppartmentId);
                            model.ClientsModel = sortClients;
                            model.AppartmentsModel = sortAppartments;
                            break;
                    }
                    return View(model);
                }
                else
                {
                    model.ClientsModel = clients;
                    model.AppartmentsModel = appartments;
                    return View(model);
                }
            }
            else
                return RedirectToAction("Enter", "User");
        }
    }
}