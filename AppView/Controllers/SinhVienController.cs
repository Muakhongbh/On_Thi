using AppAPI.Models;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly SinhVienServices _sinhVienServices;
        // Khai báo biến _sinhVienServices kiểu SinhVienServices, dùng để gửi request đến API và nhận response từ API
        public SinhVienController(SinhVienServices sinhVienServices)
        {
            _sinhVienServices = sinhVienServices;
        }
        public async Task<IActionResult> Index()
        {
            // Gửi request GET đến API để lấy danh sách sinh viên và gán kết quả vào biến sinhViens để trả về cho View
            var sinhViens = await _sinhVienServices.GetSinhViens();
            return View(sinhViens);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SinhVien sinhVien)
        {
            // Gửi request POST đến API để tạo một sinh viên mới và chuyển kết quả về dạng SinhVien và trả về cho View và hiển thị thông báo "Thêm sinh viên thành công"
            await _sinhVienServices.CreateSinhVien(sinhVien);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sinhVien = await _sinhVienServices.GetSinhVienId(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SinhVien sinhVien)
        {
            await _sinhVienServices.UpdateSinhVien(sinhVien);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSv(int id)
        {
            await _sinhVienServices.DeleteSinhVien(id);
            return RedirectToAction("Index", "SinhVien");
        }
    }
}
