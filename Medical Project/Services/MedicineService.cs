using Medical_Project.Exceptions;
using Medical_Project.Models;
using Medical_Project.Utilities;

namespace Medical_Project.Services
{
    public class MedicineService
    {

        public void CreateMedicine(Medicine medicine)
        {
            foreach (var category in DB.Categories)
            {
                if (medicine.CategoryId == category.Id)
                {
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                    DB.Medicines[DB.Medicines.Length - 1] = medicine;
                    Console.WriteLine("Medicine successfully created.");
                    return;
                }
            }
            throw new NotFoundException("Category not found.");
        }

        public void GetAllMedicines(int userId)
        {
           foreach(var medicine in DB.Medicines)
            {
                if(medicine.UserId == userId)
                {
                    Console.WriteLine(medicine);
                }
            }
        }

        public void GetMedicineById(int medicineId, int userId)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Id == medicineId && userId == medicine.UserId)
                {
                    Console.WriteLine(medicine);
                    return;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

        public void GetMedicineByName(string name, int userId)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Name == name && userId == medicine.UserId)
                {
                    Console.WriteLine(medicine);
                    return;
                }
            }
            throw new NotFoundException("Medicine with the entered name not found.");
        }

        public Medicine[] GetMedicineByCategory(int categoryId)
        {
            Medicine[] result = new Medicine[0];

            for(int i = 0; i < DB.Medicines.Length; i++) 
            {
                if (DB.Medicines[i].CategoryId == categoryId)
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[^1] = DB.Medicines[i];
                }
            }
            throw new NotFoundException("Medicine with the entered category ID not found.");
        }

        public void RemoveMedicine(int userId, int medicineId)
        {
            for (int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == medicineId && userId == DB.Medicines[i].UserId)
                {
                    for (int j = 0; j < DB.Medicines.Length - 1; j++)
                    {
                        DB.Medicines[j] = DB.Medicines[j + 1];
                    }
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
                    Color.WriteLine("Medicine successfully deleted.", ConsoleColor.Green);
                    return;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

        public void UpdateMedicine(int id, Medicine medicine, int userId)
        {
            for(int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == id && DB.Medicines[i].UserId == userId)
                {
                    DB.Medicines[i].Name = medicine.Name;
                    DB.Medicines[i].Price = medicine.Price;
                    DB.Medicines[i].CategoryId = medicine.CategoryId;
                    Color.WriteLine("Medicine successfully updated.", ConsoleColor.Green);
                    return;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

    }
}
