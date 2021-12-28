using Aranda.Services;
using Aranda.Services.Request;
using Aranda.Services.Responses;
using Aranda.Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aranda.Site.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        #region [Services]

        private readonly IUserAppServices _services;

        #endregion [Services]

        #region [Ctor]

        public UsersController(IUserAppServices services)
        {
            _services = services;
        }

        #endregion [Ctor]

        #region [Index]

        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserViewModel()
            {
                Delete = false,
                Edit = false
            };

            try
            {
                var response = GetUsersSearch(EnumSearchUser.All);
                model.Data = response;
            }
            catch (Exception ex)
            {
                model.Data.Error(ex);
            }
            return View(model);
        }

        #endregion [Index]

        #region [SearchByName]

        [HttpGet]
        public IActionResult SearchByName()
        {
            return View(new UserFilterByName());
        }

        [HttpPost]
        public IActionResult SearchByName(UserFilterByName request)
        {
            var model = new UserViewModel()
            {
                Delete = false,
                Edit = false
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var response = GetUsersSearch(EnumSearchUser.FullName, new UserAppRequest()
                    {
                        FullName = request.FullName
                    });
                    model.Data = response;
                }
            }
            catch (Exception ex)
            {
                model.Data.Error(ex);
            }
            return PartialView("_UserList", model);
        }

        #endregion [SearchByName]

        #region [SearchByRol]

        [HttpGet]
        public IActionResult SearchByRol()
        {
            return View(new UserFilterByRol());
        }

        [HttpPost]
        public IActionResult SearchByRol(UserFilterByRol request)
        {
            var model = new UserViewModel()
            {
                Delete = false,
                Edit = false
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var response = GetUsersSearch(EnumSearchUser.ByRol, new UserAppRequest()
                    {
                        RolId = request.RolId
                    });

                    model.Data = response;
                }
            }
            catch (Exception ex)
            {
                model.Data.Error(ex);
            }
            return PartialView("_UserList", model);
        }

        #endregion [SearchByRol]

        #region [EditSearch]

        [HttpGet]
        public IActionResult EditSearch(bool? search)
        {
            return View(new UserFilterByName() { Search = search });
        }

        [HttpPost]
        public IActionResult EditSearch(UserFilterByName request)
        {
            var model = new UserViewModel()
            {
                Delete = false,
                Edit = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var response = GetUsersSearch(EnumSearchUser.FullName, new UserAppRequest()
                    {
                        FullName = request.FullName
                    });

                    model.Data = response;
                }
            }
            catch (Exception ex)
            {
                model.Data.Error(ex);
            }
            return PartialView("_UserList", model);
        }

        #endregion [EditSearch]

        #region [DeleteSearch]

        [HttpGet]
        public IActionResult DeleteSearch(bool? search)
        {
            return View(new UserFilterByName() { Search = search });
        }

        [HttpPost]
        public IActionResult DeleteSearch(UserFilterByName request)
        {
            var model = new UserViewModel()
            {
                Delete = true,
                Edit = false
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var response = GetUsersSearch(EnumSearchUser.FullName, new UserAppRequest()
                    {
                        FullName = request.FullName
                    });

                    model.Data = response;
                }
            }
            catch (Exception ex)
            {
                model.Data.Error(ex);
            }
            return PartialView("_UserList", model);
        }

        #endregion [DeleteSearch]


        #region [Delete]

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var response = new UserAppResponse();
            try
            {
                if (!id.HasValue) 
                {
                    response.Error("El id es requerido");
                    return View(response);
                }

                response = _services.GetUserFromById(id.Value);
            }
            catch (Exception ex)
            {
                response.Error(ex);
            }
            return View(response);
        }

        [HttpPost]
        public IActionResult Delete(UserAppResponse request)
        {
            
            try
            {
                request = _services.DeleteUser(request.Data.Id);

                if (request.Success)
                    return RedirectToAction("DeleteSearch", "Users", new { search = true });
            }
            catch (Exception ex)
            {
                request.Error(ex);
            }
            return View(request);
        }

        #endregion [DeleteSearch]

        #region [Save]
        [HttpGet]
        public IActionResult Save(int? id)
        {
            var model = new UserModelRequest();
            try
            {
                if (id.HasValue)
                {
                    var response = _services.GetUserFromById(id.Value);

                    if (response.Success)
                        model.Ok(response.Data);
                }
            }
            catch (Exception ex)
            {
                model.Error(ex);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(UserModelRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _services.SaveUserApp(request.Data);
                    if (response.Success)
                        return RedirectToAction("EditSearch", new { search = true });
                    else
                        request.Error(response.Message);
                }
            }
            catch (Exception ex)
            {
                request.Error(ex);
            }
            return View(request);
        } 
        #endregion

        #region [Private Methods]

        private UserAppResponseList GetUsersSearch(EnumSearchUser typeSearch, UserAppRequest request = null)
        {
            var response = new UserAppResponseList();
            try
            {
                switch (typeSearch)
                {
                    case EnumSearchUser.All:
                        response = _services.GetUserAppAll();
                        break;

                    case EnumSearchUser.FullName:
                        response = _services.GetUserAppFromByFullName(request.FullName);
                        break;

                    case EnumSearchUser.ByRol:
                        response = _services.GetUserAppFromByRolId(request.RolId);
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Error(ex);
            }
            return response;
        }

        #endregion [Private Methods]
    }
}