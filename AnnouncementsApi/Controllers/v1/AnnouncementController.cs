using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnnouncementsApi.Contexts;
using AnnouncementsApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AnnouncementsApi.Wrappers;

namespace AnnouncementsApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AnnouncementContext _context;

        public AnnouncementsController(AnnouncementContext context)
        {
            _context = context;
        }

        // GET: api/Announcement
        [HttpGet]
        public async Task<Pagination<List<AnnouncementModel>>> GetAnnouncements([FromQuery] int page, [FromQuery] bool get_all)
        {
            var _route = Request.Path.Value;
            var _baseUrl = $"https://localhost:5001{_route}";
            var _queryString = "?page=";
            var _pageSize = 10;
            var _totalRecords = await _context.Announcements.CountAsync();
            var _totalPage = (int)_totalRecords / _pageSize;
            var _recordsInLastPage = (int)_totalRecords % _pageSize;
            var _page = page == 0 ? 1 : page;
            _totalPage = _totalPage == 0 ? 1 : _recordsInLastPage != 0 ? _totalPage + 1 : _totalPage;

            if (get_all)
            {
                var _allData = await _context.Announcements.ToListAsync();
                var _res = new Pagination<List<AnnouncementModel>>();
                _res.Data = _allData;
                return _res;
            }

            var _pagedRecords = await _context.Announcements
                .Skip((_page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();
            var _pagedRes = new Pagination<List<AnnouncementModel>>();
            _pagedRes.Data = _pagedRecords;
            _pagedRes.Page = _page;
            _pagedRes.TotalPage = _totalPage;
            _pagedRes.PageUrl = _baseUrl + _queryString + (_page <= _totalPage ? _page : _totalPage).ToString();
            _pagedRes.PreviousPageUrl = _page - 1 != 0 ? _baseUrl + _queryString + (_page - 1).ToString() : null;
            _pagedRes.NextPageUrl = _page + 1 <= _totalPage ? _baseUrl + _queryString + (_page + 1).ToString() : null;
            return _pagedRes;
        }

        // GET: api/Announcement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementModel>> GetAnnouncementModel(int id)
        {
            var announcementModel = await _context.Announcements.FindAsync(id);

            if (announcementModel == null)
            {
                return NotFound();
            }

            return announcementModel;
        }

        // PUT: api/Announcement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncementModel(int id, AnnouncementModel announcementModel)
        {
            if (id != announcementModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(announcementModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(GetAnnouncementModel(id).Result.Value);
        }

        // PATCH: api/Announcement/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AnnouncementModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var announcementFromDb = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == id);

            if (announcementFromDb == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(announcementFromDb, ModelState);

            var isValid = TryValidateModel(announcementFromDb);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return Ok(GetAnnouncementModel(id).Result.Value);
        }

        // POST: api/Announcement
        [HttpPost]
        public async Task<ActionResult<AnnouncementModel>> PostAnnouncementModel(AnnouncementModel announcementModel)
        {
            if (AnnouncementModelExists(announcementModel.Id))
            {
                var errorObject = new
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = 400,
                    Error = "An item with the same key has already been added."

                };
                return BadRequest("An item with the same key has already been added.");
            }
            _context.Announcements.Add(announcementModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnnouncementModel), new { id = announcementModel.Id }, announcementModel);
        }

        // DELETE: api/Announcement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncementModel(int id)
        {
            var announcementModel = await _context.Announcements.FindAsync(id);
            if (announcementModel == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcementModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementModelExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
