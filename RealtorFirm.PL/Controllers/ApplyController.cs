using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Interfaces;
using RealtorFirm.PL.Models;
using AutoMapper;

namespace RealtorFirm.PL.Controllers
{
    public class ApplyController : Controller
    {
        IAppartmentService appartmentService;
        IUserService userService;
        IClientService clientService;
        IApplyService applyService;
        public ApplyController(IAppartmentService appartmentService,
                                    IUserService userService,
                                    IClientService clientService,
                                    IApplyService applyService)
        {
            this.appartmentService = appartmentService;
            this.userService = userService;
            this.clientService = clientService;
            this.applyService = applyService;
        }

        public ActionResult Show()
        {
            if (Session["userId"] != null)
            {
                int id = int.Parse(Session["userId"].ToString());
                IEnumerable<ApplyDTO> applyDTOs = applyService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var apply = mapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);
                
                return View(apply);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Delete(int id)
        {
            if (Session["userId"] != null)
            {
                applyService.Delete(id);

                return RedirectToAction("Show");
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Deleted(int id, int app)
        {
            if (Session["userId"] != null)
            {
                applyService.Delete(id);

                return RedirectToAction("Submit", new { app = app });
            }
            else
                return RedirectToAction("Enter", "User");
        }


        public ActionResult Notifications()
        {
            if (Session["userId"] != null)
            {
                int id = int.Parse(Session["userId"].ToString());
                IEnumerable<ApplyDTO> applyDtos = applyService.GetNotifications(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var apply = mapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDtos);

                return View(apply);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult NotDelete(int id)
        {
            if (Session["userId"] != null)
            {
                applyService.Delete(id);

                return RedirectToAction("Notifications");
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Allow(ApplyDTO applyDTO)
        {
            if (Session["userId"] != null)
            {
                applyDTO.ApplyStatus = "allowed";
                applyService.Update(applyDTO);
                applyService.Delete(applyDTO.ApplyId);
                return RedirectToAction("Notifications");
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Submit(int appartment)
        {
            if (Session["userId"] != null)
            {
                AppartmentDTO appartmentDTO = appartmentService.Get(appartment);
                var appmapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, AppartmentModel>()).CreateMapper();
                var appartment_ = appmapper.Map<AppartmentDTO, AppartmentModel>(appartmentDTO);

                IEnumerable<ClientDTO> clientDtos = clientService.GetUser(Int32.Parse(Session["userId"].ToString()));
                var clientMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var clients = clientMapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientDtos);
                ViewBag.AppId = appartment_.AppartmentId;
                return View(clients);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Add(int cl, int appartment)
        {
            if (Session["userId"] != null)
            {
                int user = int.Parse(Session["userId"].ToString());
                AppartmentDTO appartment_ = appartmentService.Get(appartment);
                ClientDTO client = clientService.Get(appartment_.ClientId);
                IEnumerable<ApplyDTO> applyDtos = applyService.GetSender(cl);
                int clientId = cl;
                if (applyDtos.Count() < 5)
                {
                    applyService.Add(cl, appartment_, user, client.UserId);

                    return RedirectToAction("Details", "Appartment", new { id = appartment_.AppartmentId });
                }
                else
                    return RedirectToAction("Five", "Apply", new { clientId = clientId, app = appartment });
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Five(int clientId, int appartment)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<ApplyDTO> applyDTOs = applyService.GetClient(clientId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var applies = mapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);
                ViewBag.AppId = appartment;
                return View(applies);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (Session["userId"] != null)
            {
                int id = int.Parse(Session["userId"].ToString());
                IEnumerable<ApplyDTO> applyDTOs = applyService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var applies = mapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);
                return View(applies);
            }
            else
                return RedirectToAction("Enter", "User");
        }

        [HttpPost]
        public ActionResult Search(int price, int rooms)
        {
            if (Session["userId"] != null)
            {
                IEnumerable<ApplyDTO> applyDTOs = applyService.Search(price, rooms);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, ApplyModel>()).CreateMapper();
                var applies = mapper.Map<IEnumerable<ApplyDTO>, List<ApplyModel>>(applyDTOs);
                return View(applies);
            }
            else
                return RedirectToAction("Enter", "User");
        }
    }
}