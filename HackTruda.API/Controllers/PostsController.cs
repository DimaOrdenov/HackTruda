using AutoMapper;
using HackTruda.API.Models;
using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public PostsController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPost()
        {
            var result = _mapper.Map<IEnumerable<PostResponse>>(await _context.Post.ToListAsync());
            return new JsonResult(result);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> GetPost(int id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostResponse>(post);
        }

        // GET: api/Posts/users/5
        [HttpGet("users/{id}")]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetUsersPost(int id)
        {
            var user = await _context.Users
                .Where(x => x.UserId == id)
                .Include(p => p.Posts)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return new JsonResult(_mapper.Map<IEnumerable<PostResponse>>(user.Posts));
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostRequest post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostResponse>> PostPost(PostRequest post)
        {
            _context.Post.Add(_mapper.Map<Post>(post));
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostResponse>> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponse>(post);
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}