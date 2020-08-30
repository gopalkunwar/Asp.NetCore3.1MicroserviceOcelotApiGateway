using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Course.Microservice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Course.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses =await _context.Courses.ToListAsync();
            if (courses == null) NotFound();
            return Ok(courses);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return Ok(course.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Models.Course courseDb)
        {
            var course = await _context.Courses.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (course == null) return NotFound();
            else
            {
                course.Id = courseDb.Id;
                course.Name = courseDb.Name;
                await _context.SaveChangesAsync();
                return Ok(course.Id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(course.Id);
        }
    }
}
