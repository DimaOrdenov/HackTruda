using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackTruda.API.Models;
using HackTruda.DataModels.Requests;
using AutoMapper;
using HackTruda.DataModels.Responses;

namespace HackTruda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public NotificationsController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, NotificationRequest notification)
        {
            if (id != notification.NotificationId)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notifications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            //  _context.Notification.Add(_mapper.Map<Notification>(notification));
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.NotificationId }, notification);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> DeleteNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return notification;
        }
        /// <summary>
        /// Возвращает ленту пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="page">страница</param>
        /// <returns></returns>
        // GET: api/Posts/users/5
        [HttpGet("notifications/{id}")]
        public async Task<ActionResult<IEnumerable<NotificationResponse>>> GetUserNotifications(int id)
        {
            // TODO: добавить загрузку постов друзей и подписок
            var notifications = await _context.Notification
                .AsNoTracking()
                .Where(x => x.RecipientId == id)
                .Include(u=>u.Sender)
                .ToListAsync();

           
            return new JsonResult(_mapper.Map<IEnumerable<NotificationResponse>>(notifications));
        }
        private bool NotificationExists(int id)
        {
            return _context.Notification.Any(e => e.NotificationId == id);
        }
    }
}
