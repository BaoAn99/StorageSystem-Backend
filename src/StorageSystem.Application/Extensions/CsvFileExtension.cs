using CsvHelper;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;
using System.Globalization;
using System.Text;
using System.Transactions;

namespace StorageSystem.Application.Extensions
{
    public static class CsvFileExtension
    {
        public static List<T> Import<T>(string filePath) where T : class
        {
            List<T> records = new List<T>();

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = new List<T>(csv.GetRecords<T>());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi xảy ra: {ex.Message}");
            }

            return records;
        }

        public static void Export(string path)
        {
            int totalRecords = 600_000;
            //int totalRecords = 10_000_000;
            int batchSize = 100_000;

            Console.WriteLine("Bắt đầu tạo file CSV...");

            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine("Id,Name,Description,CreatedAt,CreatedByUserId,CreatedByName,UpdatedAt,UpdatedByUserId,UpdatedByName,IsDeleted,IsPublished");

                for (int i = 1; i <= totalRecords; i++)
                {
                    Guid id = Guid.NewGuid();
                    string name = $"Name_{i}";
                    string createdAt = "2024-10-12";
                    string createdByUserId = "KhuongPham";
                    string createdByName = "Pham Duy Khuong";
                    string updatedAt = "2024-10-12";
                    string updatedByUserId = "KhuongPham";
                    string updatedByName = "Pham Duy Khuong";
                    bool isDeleted = false;
                    bool isPublished = true;
                    string description = $"Description {i}";

                    writer.WriteLine($"{id},{name},{description},{createdAt},{createdByUserId},{createdByName},{updatedAt},{updatedByUserId},{updatedByName},{isDeleted},{isPublished}");

                    if (i % batchSize == 0)
                    {
                        Console.WriteLine($"{i} bản ghi đã được tạo.");
                    }
                }
            }

            Console.WriteLine("Đã hoàn thành tạo file CSV.");
        }
    }
}
