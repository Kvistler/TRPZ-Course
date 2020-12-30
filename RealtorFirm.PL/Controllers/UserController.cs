using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Interfaces;
using RealtorFirm.PL.Models;
using AutoMapper;

namespace RealtorFirm.PL.Controllers
{
    public class UserController : Controller
    {
        IAppartmentService appartmentService;
        IUserService userService;
        IClientService clientService;
        IApplyService applyService;
        public UserController(IAppartmentService appartmentService,
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
        public ActionResult Enter()
        {
            return View();
        }

        public ActionResult Entered()
        {
            ViewBag.Id = Session["userId"].ToString();
            ViewBag.Login = Session["userLogin"].ToString();
            return RedirectToAction("Page");
        }

        [HttpPost]
        public ActionResult Enter(string Login, string Password)
        {
            UserDTO userDTOs = userService.Get(Login, Password);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
            var user = mapper.Map<UserDTO, UserModel>(userDTOs);
           
            ViewBag.User = user;

            if (user != null)
            {
                Session["userId"] = user.UserId;
                Session["userLogin"] = user.Login;
                Session["userPassword"] = user.Password;
                return RedirectToAction("Entered");
            }
            else
                return RedirectToAction("Enter");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Registered()
        {
            ViewBag.Id = Session["userId"].ToString();
            ViewBag.Login = Session["userLogin"].ToString();
            return RedirectToAction("Page");
        }

        [HttpPost]
        public ActionResult Registration(UserDTO user, string confirm)
        {
            
            if (user != null && user.Password == confirm)
            {
                    userService.Add(user);
                    UserDTO userDTOs = userService.Get(user.Login, user.Password);
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                    var user_ = mapper.Map<UserDTO, UserModel>(userDTOs);

                    ViewBag.User = user_;

                    Session["userId"] = user_.UserId;
                    Session["userLogin"] = user_.Login;
                    return RedirectToAction("Registered");
            }
            else
                return RedirectToAction("Registration");
        }

        [HttpGet]
        public ActionResult Page()
        {
            if (Session["userId"] != null)
            {
                string id = Session["userId"].ToString();
                ViewBag.S = id;
                UserDTO userDTOs = userService.Get(Int32.Parse(id));
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                var user = mapper.Map<UserDTO, UserModel>(userDTOs);
                
                ViewBag.User = user;

                IEnumerable<ClientDTO> clientDTOs = clientService.GetUser(Int32.Parse(id));
                var clientMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
                var clients = clientMapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientDTOs);

                return View(clients);
            }
            else
                return RedirectToAction("Enter", "User");

        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (Session["userId"] != null)
            {
                UserDTO userDTOs = userService.Get(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                var user = mapper.Map<UserDTO, UserModel>(userDTOs);

                ViewBag.User = user;

                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }
        [HttpPost]
        public ActionResult Update(UserDTO user, int id)
        {
            if (Session["userId"] != null)
            {
                userService.Update(user);
                ViewBag.Id = id;
                int? oldId = id;
                UserDTO userDTOs = userService.Get(user.Login, user.Password);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
                var user_ = mapper.Map<UserDTO, UserModel>(userDTOs);

                ViewBag.User = user_;
          
                return RedirectToAction("Page", new { id = id});
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult Delete(UserDTO user, int id)
        {
            if (Session["userId"] != null)
            {
                int userId = int.Parse(Session["userId"].ToString());
                IEnumerable<ClientDTO> clientDTOs = clientService.GetUser(userId);
                IEnumerable<AppartmentDTO> appartmentDTOs;
                IEnumerable<ApplyDTO> applyDTOs;
                foreach (var n in clientDTOs)
                {
                        clientService.DeleteUser(n);
                        appartmentDTOs= appartmentService.GetForClient(n.ClientId);
                        applyDTOs = applyService.GetClient(n.ClientId);
                    foreach (var k in appartmentDTOs)
                    {
                        appartmentService.Delete(k.AppartmentId);
                    }

                    foreach (var d in applyDTOs)
                    {
                        applyService.Delete(d.ApplyId);
                    }

                }
                userService.Delete(id);
                 
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }

        public ActionResult LogOut()
        {
            if (Session["userId"] != null)
            {
                Session["userId"] = null;
                Session["login"] = null;
                return View();
            }
            else
                return RedirectToAction("Enter", "User");
        }

    }
}