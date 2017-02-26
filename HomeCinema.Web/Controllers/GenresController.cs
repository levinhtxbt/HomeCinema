using HomeCinema.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repository;
using HomeCinema.Entities.Models;
using HomeCinema.Web.Models;
using AutoMapper;

namespace HomeCinema.Web.Controllers
{
    [RoutePrefix("api/genres")]
    [Authorize(Roles = "Admin")]
    public class GenresController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Genre> _genresRepository;

        public GenresController(
            IEntityBaseRepository<Error> errorsRepository, 
            IUnitOfWork unitOfWork,
            IEntityBaseRepository<Genre> genresRepository) : 
            base(errorsRepository, unitOfWork)
        {
            _genresRepository = genresRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var genres = _genresRepository.GetAll().ToList();
                IEnumerable<GenreViewModel> genresVM = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(genres);
                response = request.CreateResponse<IEnumerable<GenreViewModel>>(HttpStatusCode.OK, genresVM);

                return response;
            });

            
        }
        
    }
}