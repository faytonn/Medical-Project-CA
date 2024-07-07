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

        public Medicine GetMedicineById(int medicineId, int userId)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Id == medicineId && userId == medicine.UserId)
                {
                    Console.WriteLine(medicine);
                    return medicine;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

        public Medicine GetMedicineByName(string medicineName, int userId)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Name == medicineName && userId == medicine.UserId)
                {
                    Console.WriteLine(medicine);
                    return medicine;
                }
            }
            throw new NotFoundException("Medicine with the entered name not found.");
        }

        public Medicine[] GetMedicineByCategory(int userId, int categoryId)
        {
            Medicine[] result = new Medicine[0];

            for(int i = 0; i < DB.Medicines.Length; i++) 
            {
                if (DB.Medicines[i].CategoryId == categoryId && DB.Medicines[i].UserId == userId)
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[^1] = DB.Medicines[i];
                }
            }
            throw new NotFoundException("Medicine with the entered category ID not found.");
        }

        public Medicine RemoveMedicine(int userId, int medicineId)
        {
            for (int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == medicineId && userId == DB.Medicines[i].UserId)
                {
                    Medicine removedMedicine = DB.Medicines[i];

                    for (int j = 0; j < DB.Medicines.Length - 1; j++)
                    {
                        DB.Medicines[j] = DB.Medicines[j + 1];
                    }
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
                    Color.WriteLine("Medicine successfully deleted.", ConsoleColor.Green);
                    return removedMedicine;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

        public Medicine UpdateMedicine(int id, Medicine medicine, int userId)
        {
            for(int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == id && DB.Medicines[i].UserId == userId)
                {
                    Medicine updatedMedicine = DB.Medicines[i];

                    DB.Medicines[i].Name = medicine.Name;
                    DB.Medicines[i].Price = medicine.Price;
                    DB.Medicines[i].CategoryId = medicine.CategoryId;
                    Color.WriteLine("Medicine successfully updated.", ConsoleColor.Green);
                    return updatedMedicine;
                }
            }
            throw new NotFoundException("Medicine with the entered ID not found.");
        }

    }
}
