using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BELAJAR_APII.Models;

namespace BELAJAR_APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SekolahDbContext _context;

        public StudentsController(SekolahDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiswaDtoName>>> GetStudents()
        {
            var siswa = _context.Students
                .Select(s => new
            {
                s.Id,
                s.Nama,
                Sekolah = s.Sekolah.Nama,
                s.KotaId
                
            }).ToList();
            return Ok(siswa);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiswaDTO>> GetStudent(int id)
        {
            var siswa = _context.Students
                 .Where(s => s.Id == id)
                 .Select(s => new
                 {
                    s.Id,
                    s.Nama,
                    kota = s.Kota.Nama,
                    sekolah = s.Sekolah.Nama
                    

                 }).FirstOrDefault();

            if (siswa == null)
            {
                return NotFound(new {message = "Data tidak ditemukan"});
            }

            return Ok(siswa);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, SiswaDTO student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            student.Name = student.Name;
            student.SekolahID = student.SekolahID;
            student.KotaID = student.KotaID;
            _context.Entry(student).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<SiswaDTO>> PostStudent(SiswaDTO siswaDto)
        {

            var siswa = new Student
            {
                Nama = siswaDto.Name,
                SekolahId = siswaDto.SekolahID,
                KotaId =siswaDto.KotaID,
            };

            _context.Students.Add(siswa);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetStudent", new { id = siswa.Id },siswaDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
