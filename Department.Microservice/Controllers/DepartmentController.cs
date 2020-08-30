using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Department.Microservice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Department.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _context.Departments.ToListAsync();
            if (departments == null) return NotFound();
            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return Ok(department.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department == null) return NotFound();
            return Ok(department);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Models.Department departmentDb)
        {
            var department = await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department == null) return NotFound();
            else
            {
                department.Id = departmentDb.Id;
                department.Name = departmentDb.Name;
                await _context.SaveChangesAsync();
                return Ok(department.Id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department =await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department == null) return NotFound();
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Ok(department.Id);
        }


    }
}
