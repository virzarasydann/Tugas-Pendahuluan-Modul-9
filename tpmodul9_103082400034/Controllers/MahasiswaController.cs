using tpmodul9_103082400034.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace tpmodul9_103082400034.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahasiswaController : ControllerBase
    {
        private static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>
        {
            new Mahasiswa { Nim = "1302000001", Nama = "LeBron James" },
            new Mahasiswa { Nim = "1302000002", Nama = "Stephen Curry" },
        };

        
        [HttpGet]
        public ActionResult<IEnumerable<Mahasiswa>> GetAllMahasiswa()
        {
            return Ok(daftarMahasiswa);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Mahasiswa> GetMahasiswaByIndex(int id)
        {
            if (id < 0 || id >= daftarMahasiswa.Count)
                return NotFound();

            return Ok(daftarMahasiswa[id]);
        }

       
        [HttpPost]
        public ActionResult<Mahasiswa> CreateMahasiswa(Mahasiswa mahasiswaBaru)
        {
           
            if (string.IsNullOrEmpty(mahasiswaBaru.Nama) || string.IsNullOrEmpty(mahasiswaBaru.Nim))
                return BadRequest("Nama dan Nim harus diisi.");

            if (daftarMahasiswa.Any(m => m.Nim == mahasiswaBaru.Nim))
                return BadRequest($"Nim {mahasiswaBaru.Nim} sudah terdaftar.");

           
            daftarMahasiswa.Add(mahasiswaBaru);

            
            int indexBaru = daftarMahasiswa.Count - 1;

           
            return CreatedAtAction(nameof(GetMahasiswaByIndex), new { id = indexBaru }, mahasiswaBaru);
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdateMahasiswa(int id, Mahasiswa mahasiswaUpdate)
        {
            
            if (id < 0 || id >= daftarMahasiswa.Count)
                return NotFound($"Mahasiswa dengan index {id} tidak ditemukan.");

           
            if (string.IsNullOrEmpty(mahasiswaUpdate.Nama) || string.IsNullOrEmpty(mahasiswaUpdate.Nim))
                return BadRequest("Nama dan Nim harus diisi.");

          
            var mahasiswaLama = daftarMahasiswa[id];
            if (mahasiswaUpdate.Nim != mahasiswaLama.Nim &&
                daftarMahasiswa.Any(m => m.Nim == mahasiswaUpdate.Nim))
                return BadRequest($"Nim {mahasiswaUpdate.Nim} sudah terdaftar oleh mahasiswa lain.");

           
            mahasiswaLama.Nim = mahasiswaUpdate.Nim;
            mahasiswaLama.Nama = mahasiswaUpdate.Nama;

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMahasiswa(int id)
        {
            if (id < 0 || id >= daftarMahasiswa.Count)
                return NotFound();

            daftarMahasiswa.RemoveAt(id);
            return NoContent();
        }
    }
}