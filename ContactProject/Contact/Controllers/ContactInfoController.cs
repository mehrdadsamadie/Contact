using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Entities.Model;
using Contact.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Infrastructure;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        // GET: api/ContactInfo
        private readonly IRepositoryWrapper _repoWrapper;

        public ContactInfoController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public ContactListView Get(int page = 1, int pageSize = 10, string searchStr = null, string column = "Name", string sort = "asc")
        {
            try
            {
                var _contactInfos = _repoWrapper.ContactInfo.FindByCondition(x =>
                (string.IsNullOrEmpty(searchStr) || x.FullName.ToLower().Contains(searchStr.ToLower())));
                
                
                int _total = _contactInfos.Count();
                switch (sort)
                {
                    case "asc":
                        _contactInfos = _contactInfos.OrderByProperty(column);
                        break;
                    case "desc":
                        _contactInfos = _contactInfos.OrderByPropertyDescending(column);
                        break;
                }
                if (_contactInfos != null)
                {
                    var _list = _contactInfos.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new ContactView()
                    {
                        AddressId = x.Addresses.FirstOrDefault() == null ? 0 : x.Addresses.FirstOrDefault().AddressId,
                        City = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().City,
                        Country = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().Country,
                        PostalCode = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().PostalCode,
                        State = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().State,
                        ContactId = x.ContactId,
                        Street1 = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().Street1,
                        Street2 = x.Addresses.FirstOrDefault() == null ? null : x.Addresses.FirstOrDefault().Street2,
                        Email = x.Email,
                        Gender = x.Gender,
                        LastName = x.LastName,
                        Name = x.Name,
                        Note = x.Note,
                        Phone = x.Phone,
                        WebSite = x.WebSite,
                        Image = x.Image,
                        FullName = x.FullName,
                        ImageShow = Request.Scheme + "://" + Request.Host+ x.Image,
                    }).ToList();
                    return (new ContactListView() { List = _list, Total = _total });
                }
                else { return null; }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        // GET: api/ContactInfo/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var _contactInfo = _repoWrapper.ContactInfo.FindByCondition(x => x.ContactId == id).FirstOrDefault();
                if (_contactInfo != null)
                {
                    var _contactView = new ContactView()
                    {
                        City = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().City,
                        Country = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().Country,
                        State = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().State,
                        PostalCode = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().PostalCode,
                        Street2 = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().Street2,
                        Street1 = _contactInfo.Addresses.FirstOrDefault() == null ? null : _contactInfo.Addresses.FirstOrDefault().Street1,
                        AddressId = _contactInfo.Addresses.FirstOrDefault() == null ? 0 : _contactInfo.Addresses.FirstOrDefault().AddressId,
                        WebSite = _contactInfo.WebSite,
                        Phone = _contactInfo.WebSite,
                        ContactId = _contactInfo.ContactId,
                        Email = _contactInfo.Email,
                        Gender = _contactInfo.Gender,
                        Image = _contactInfo.Image,
                        LastName = _contactInfo.LastName,
                        Name = _contactInfo.Name,
                        Note = _contactInfo.Note,
                        ImageShow = Request.Scheme + "://" + Request.Host + _contactInfo.Image,
                    };
                    return Ok(new { _contactView });
                }
                else
                {
                    return BadRequest("User object is null");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }

        }

        //public async Task<ActionResult<ContactView>> GetContactView(int id, ContactView model)
        //{
        //    return model;
        //}

        // POST: api/ContactInfo

        [HttpPost]
        public IActionResult Post([FromBody]ContactView model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var _address = new Address()
                {
                    ContactInfo = new ContactInfo()
                    {
                        ContactId = model.ContactId,
                        Email = model.Email,
                        Gender = model.Gender,
                        Image = model.Image,
                        LastName = model.LastName,
                        Name = model.Name,
                        Note = model.Note,
                        Phone = model.Phone,
                        WebSite = model.WebSite,
                    },
                    AddressId = model.AddressId,
                    City = model.City,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    State = model.State,
                    Street1 = model.Street1,
                    Street2 = model.Street2,
                };

                if (model.ContactId == 0)
                {
                    _address = _repoWrapper.Address.Create(_address);
                    _repoWrapper.Save();
                }
                else
                {
                    _repoWrapper.Address.Update(_address);
                    _repoWrapper.Save();
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT: api/ContactInfo/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _contact = _repoWrapper.ContactInfo.FindByCondition(x => x.ContactId == id).FirstOrDefault();
                if (_contact == null)
                { return BadRequest("User object is null"); }
                else
                {
                    var _addresses = _repoWrapper.Address.FindByCondition(x => x.ContactId == _contact.ContactId);
                    if (_addresses != null)
                    {
                        foreach(var item in _addresses)
                        {
                            _repoWrapper.Address.Delete(item);
                        }
                    }
                    _repoWrapper.ContactInfo.Delete(_contact);
                    _repoWrapper.Save();
                    return StatusCode(201);
                }
            }
            catch (Exception e){ return StatusCode(500, $"Internal server error: {e}"); }
        }
    }
}
