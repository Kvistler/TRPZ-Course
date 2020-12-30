using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Interfaces;
using RealtorFirm.PL.Models;
using AutoMapper;


namespace RealtorFirm.PL.Controllers
{
    public class AppartmentController : Controller
    {
        IAppartmentService appartmentService;
        IUserService userService;
        IClientService clientService;
        IApplyService applyService;
        public AppartmentController(IAppartmentService appartmentService,
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
        public ActionResult Add(int id)
        {
            if (Session["userId"] != null)
            {
                ViewBag.Id = id;
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }

        [HttpPost]
        public ActionResult Add(AppartmentDTO appartments)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetForClient(appartments.ClientId);
                int id = appartments.ClientId;
                if (appartmentDTOs.Count() < 5)
                {
                    appartmentService.Add(appartments);
                    return RedirectToAction("Contacts", "Client", new { id = id });
                }
                else
                    return RedirectToAction("Five", "Appartment", new { id = id });
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Five(int id)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<AppartmentDTO> appartmentDTOs = appartmentService.GetForClient(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var apps = mapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appartmentDTOs);
                return View(apps);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Deleted(int id2, int id)
        {
            if (Session["userId"] != null)
            {
                appartmentService.Delete(id2);
                IEnumerable<ApplyDTO> applyDto = applyService.GetAppartment(id2);
                foreach (var n in applyDto)
                {
                    applyService.Delete(n.ApplyId);
                }

                return RedirectToAction("Contacts", "Client", new { id = id });
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpGet]
        public ActionResult Update(int id, int clientId)
        {
            if (Session["userId"] != null)
            {
                AppartmentDTO appartmentDTOs = appartmentService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartment = mapper.Map<AppartmentDTO, AppartmentModel>(appartmentDTOs);
                ViewBag.Client = clientId;
                ViewBag.Appartment = appartment;
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpPost]
        public ActionResult Update(AppartmentDTO appartments)
        {
            if (Session["userId"] != null)
            {
                appartmentService.Update(appartments);
                
                IEnumerable<ApplyDTO> applies = applyService.GetAppartment(appartments.AppartmentId);
                
                if (applies.Count() != 0)
                {
                    foreach (var n in applies)
                    {
                        n.Description = appartments.Description;
                        n.OwnerId = appartments.ClientId;
                        n.Price = appartments.Price;
                        n.Rooms = appartments.Rooms;
                        n.Status = appartments.Status;
                        n.City = appartments.City;
                        n.Address = appartments.Address;

                        applyService.Update(n);
                        applyService.Delete(n.ApplyId);
                    }
                   
                }
                var id = appartments.ClientId;
                return RedirectToAction("Contacts", "Client", new { id = id });
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Search()
        {
            if (Session["userId"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult ResultSearch(int? price1, int? price2, string city, int? rooms)
        {
            if (Session["userId"] != null)
            {
                ViewBag.Id = Session["userId"];
                city = city.ToLower();
                IEnumerable<AppartmentDTO> appDTOs = appartmentService.GetBySearch(price1, price2, city, rooms);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = mapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appDTOs);
                return View(appartments);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult All(string sort, string keyword)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<AppartmentDTO> appDtos = appartmentService.GetAll();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartments = mapper.Map<IEnumerable<AppartmentDTO>, List<AppartmentModel>>(appDtos);
                var sortAppartments = from a in appartments
                               select a;
                switch (sort)
                {
                    case "price":
                        sortAppartments = sortAppartments.OrderBy(a => a.Price);
                        break;

                    case "without":
                        sortAppartments = sortAppartments.OrderBy(s => s.AppartmentId);
                        break;

                    case "type":
                        sortAppartments = sortAppartments.GroupBy(a => a.Rooms).SelectMany(a => a).OrderBy(a => a.Rooms);
                        break;

                    case "keyword":
                        if (!ReferenceEquals(keyword, null))
                            sortAppartments = sortAppartments.Where(
                                a => a.Address.ToLower().StartsWith(keyword.ToLower()) || a.City.ToLower().StartsWith(keyword.ToLower())
                                || a.Price.ToString().StartsWith(keyword)); ;
                        break;

                    default:
                        sortAppartments = sortAppartments.OrderBy(s => s.AppartmentId);
                        break;
                }

                return View(sortAppartments);
            }
            else
                return RedirectToAction("Enter", "User");
        }
      
        public ActionResult Details(int id)
        {
            if (Session["userId"] != null)
            {
                AppartmentDTO appartmentDTO = appartmentService.Get(id);
                var appartmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartment = appartmentMapper.Map<AppartmentDTO, AppartmentModel>(appartmentDTO);

                ClientDTO clientDTO = clientService.Get(appartmentDTO.ClientId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var client = mapper.Map<ClientDTO, ClientModel>(clientDTO);

                int? userId = clientDTO.UserId;
                UserDTO userDTO = userService.Get(userId);
                var userMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                var user = userMapper.Map<UserDTO, UserModel>(userDTO);

                ViewBag.Client = client;
                ViewBag.User = user;
                ViewBag.Appartment = appartment;
                ViewBag.AppId = appartment.AppartmentId;
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }
       
    }
}