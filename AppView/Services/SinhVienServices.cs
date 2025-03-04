using AppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Services
{
    public class SinhVienServices
    {
        private readonly HttpClient _httpClient;
        // Khai báo biến _httpClient kiểu HttpClient, dùng để gửi request đến API và nhận response từ API 
        public SinhVienServices(IHttpClientFactory httpClientFactory)
        {
            // Khởi tạo biến _httpClient thông qua httpClientFactory và lấy client có tên là "sinhVienAPI" để gửi request đến API
            _httpClient = httpClientFactory.CreateClient("sinhVienAPI");
        }
        public async Task<List<AppAPI.Models.SinhVien>> GetSinhViens()
        {
            // Gửi request GET đến API để lấy danh sách sinh viên và chuyển kết quả về dạng List và gán vào biến sinhViens để trả về cho Controller
            return await _httpClient.GetFromJsonAsync<List<AppAPI.Models.SinhVien>>("sinhvien");
        }
        public async Task<AppAPI.Models.SinhVien> GetSinhVienId(int id)
        {
            // Gửi request GET đến API để lấy sinh viên có Id = id và gán kết quả vào biến sinhVien để trả về cho Controller
            return await _httpClient.GetFromJsonAsync<SinhVien>($"sinhvien/{id}");
        }
        public async Task<SinhVien> CreateSinhVien(SinhVien sinhVien)
        {
            // Gửi request POST đến API để tạo một sinh viên mới và chuyển kết quả về dạng SinhVien và trả về cho Controller 
            var response = await _httpClient.PostAsJsonAsync("sinhvien", sinhVien);
            response.EnsureSuccessStatusCode();
            // Kiểm tra xem response có phải là một response thành công không
            return await response.Content.ReadFromJsonAsync<SinhVien>();
        }
        public async Task UpdateSinhVien(SinhVien sinhVien)
        {
            var response = await _httpClient.PutAsJsonAsync($"sinhvien/{sinhVien.Id}", sinhVien);
            // kiểm tra xem response có phải là một response thành công không
            // Nếu không phải thì ném ra một ngoại lệ với thông báo "Không thể cập nhật sinh viên có Id = {sinhVien.Id}"
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Không thể cập nhật sinh viên có Id = {sinhVien.Id}");
            }
        }
        public async Task DeleteSinhVien(int id)
        {
            // Gửi request DELETE đến API để xóa sinh viên có Id = id và kiểm tra xem response có phải là một response thành công không 
            var response = await _httpClient.DeleteAsync($"sinhvien/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
