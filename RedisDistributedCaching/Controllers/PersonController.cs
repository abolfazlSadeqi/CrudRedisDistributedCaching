using AutoMapper;
using DAL.Model;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RedisDistributedCaching.Dto;
using RedisDistributedCaching.Helper;

namespace RedisDistributedCaching.Controllers
{
    public class PersonController : Controller
    {
        private readonly IDistributedCache _DistributedCache;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public PersonController(IDistributedCache DistributedCache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _DistributedCache = DistributedCache;
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: PersonController

        public async Task<ActionResult> Index()
        {

            if (!_DistributedCache.TryGetValue(ListCache.PersonCacheKey, out IEnumerable<PersonDto>? PersonDtos))
            {
                var Persons = unitOfWork.Person.GetAll();
                var newperson = _mapper.Map<List<PersonDto>>(Persons);
                await _DistributedCache.SetAsync(ListCache.PersonCacheKey, newperson);
                return View(newperson);
            }

            return View(PersonDtos);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            if (!_DistributedCache.TryGetValue(ListCache.PersonCacheKey, out IEnumerable<PersonDto>? PersonDtos))
            {
                var Persons = unitOfWork.Person.GetById(id);
                var newperson = _mapper.Map<PersonDto>(Persons);

                return View(newperson);
            }

            return View(PersonDtos.FirstOrDefault(d => d.ID == id));
           
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonDto person)
        {
            try
            {
                person.CreateDate = DateTime.Now;
                var newperson = _mapper.Map<Person>(person);
                unitOfWork.Person.Add(newperson);

                unitOfWork.Save();
                _DistributedCache.Remove(ListCache.PersonCacheKey);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            if (!_DistributedCache.TryGetValue(ListCache.PersonCacheKey, out IEnumerable<PersonDto>? PersonDtos))
            {
                var Persons = unitOfWork.Person.GetById(id);
                var newperson = _mapper.Map<PersonDto>(Persons);

                return View(newperson);
            }

            return View(PersonDtos.FirstOrDefault(d => d.ID == id));
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PersonDto person)
        {
            try
            {

                var _p = unitOfWork.Person.GetById(person.ID);

                var newperson = _mapper.Map<Person>(person);
                _p.FirstName = newperson.FirstName;
                _p.LastName = newperson.LastName;
                _p.Suffix = newperson.Suffix;
                _p.Email = newperson.Email;
                _p.AdditionalContactInfo = newperson.AdditionalContactInfo;

                _p.ModifiedDate = DateTime.Now;


                unitOfWork.Save();
                _DistributedCache.Remove(ListCache.PersonCacheKey);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
