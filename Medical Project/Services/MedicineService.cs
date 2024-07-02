using Medical_Project.Models;

namespace Medical_Project.Services
{
    public class MedicineService
    {
        public void CreateMedicine(Medicine medicine)
        {
            foreach (var category in DB.Categories)
            {
                if (category.Id == medicine.CategoryId)
                {
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                    DB.Medicines[DB.Medicines.Length - 1] = medicine;
                    Console.WriteLine("Medicine successfully created.");
                }
            }
            throw new Exception("Category not found.");
        }

        public Medicine[] GetAllMedicines()
        {
            return DB.Medicines;
        }

        public Medicine GetMedicineById(int id)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Id == id)
                {
                    return medicine;
                }
            }
            throw new Exception("Medicine with the entered ID not found.");
        }

        public Medicine GetMedicineByName(string name)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.Name == name)
                {
                    return medicine;
                }
            }
            throw new Exception("Medicine with the entered name not found.");
        }

        public Medicine GetMedicineByCategory(int categoryId)
        {
            foreach (var medicine in DB.Medicines)
            {
                if (medicine.CategoryId == categoryId)
                {
                    return medicine;
                }
            }
            throw new Exception("Medicine with the entered category ID not found.");
        }

        public Medicine RemoveMedicine(int id, Medicine removedMedicine)
        {
            for (int i = 0; i < DB.Medicines.Length; i++)
            {
                if (DB.Medicines[i].Id == id)
                {
                    for (int j = 0; j < DB.Medicines.Length - 1; j++)
                    {
                        DB.Medicines[j] = DB.Medicines[j + 1];
                    }
                    Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
                    Console.WriteLine("Medicine successfully deleted.");
                    return removedMedicine;
                }
            }
            throw new Exception("Medicine with the entered ID not found.");
        }

        public void UpdateMedicine(int id, Medicine updatedMedicine)
        {
            foreach(var medicine in DB.Medicines)
            {
                if (medicine.Id == id)
                {
                    medicine.Name = updatedMedicine.Name;
                    medicine.Price = updatedMedicine.Price;
                    
                }
            }
            throw new Exception("Medicine with the entered ID not found.");
        }

    }
}
