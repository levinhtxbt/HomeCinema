using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repository;
using HomeCinema.Entities.Models;
using HomeCinema.Services;
using HomeCinema.Services.Abstract;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) :
            base(errorsRepository, unitOfWork)
        {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel loginVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(loginVM.Username, loginVM.Password);
                    if (_userContext.User != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registerVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    User _user = _membershipService.CreateUser(registerVM.Username, registerVM.Password, registerVM.Email, new int[] { 1 });
                    if (_user == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

                return response;
            });
        }
    }
}