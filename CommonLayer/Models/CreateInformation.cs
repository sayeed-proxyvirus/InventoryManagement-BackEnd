namespace InventoryManagement.CommonLayer.Models
{
    public class IItemCreateInformationRequest
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string SubCatname { get; set; }
        public string Unit { get; set; }

        public int OpeningStock { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderStock { get; set; }
        public int MaxStock { get; set; }

        public int LastPurRate { get; set; }
        public DateOnly LastPurDate { get; set; }

        public string PrefAlt { get; set; }
        public int Amount { get; set; }
        public string HScode { get; set; }

        public bool IsActive { get; set; }
        public DateOnly CreatedAT { get; set; }
        public DateOnly ExpiryDate { get; set; }
    }
    public class SupplierInfoCreateInformationRequest
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class ItemCatCreateInfoCreateInformationRequest 
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class SubCatCreateInfoCreateInformationRequest
    {
        public int SubCatID { get; set; }
        public string CategoryName { get; set; }
        public string SubCatName { get; set; }
    }


    public class CreateInformationResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
