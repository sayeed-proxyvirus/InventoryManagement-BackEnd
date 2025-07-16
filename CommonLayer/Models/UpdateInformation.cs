namespace InventoryManagement.CommonLayer.Models
{
    public class ItemUpdateInformationRequest
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string SubCatName { get; set; }
        //public string Unit { get; set; }

        public int OpeningStock { get; set; }
        public int ReorderStock { get; set; }
        public int MaxStock { get; set; }

        public string PrefAlt { get; set; }
        public string HScode { get; set; }

        public bool IsActive { get; set; }
        public DateOnly CreatedAT { get; set; }
    }
    public class GDUpdateInformationRequest
    {
        public int GDID { get; set; }
        public string GDName { get; set; }
        public string GDPhone { get; set; }
        public string GDAddress { get; set; }
    }
    public class ItemTransUpdateInformationRequest
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string SubCatName { get; set; }
        public string Unit { get; set; }

        public int CurrentStock { get; set; }
        public int LastPurRate { get; set; }
        public DateOnly LastPurDate { get; set; }
        public string EntryGoDown { get; set; }
        public int Amount { get; set; }
        public DateOnly OrderIssueDate { get; set; }
        public DateOnly OrderRecDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
    }

    public class SupplierUpdateInformationRequest
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class ItemCatUpdateInformationRequest
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class SubCatUpdateInformationRequest
    {
        public int SubCatID { get; set; }
        public string CategoryName { get; set; }
        public string SubCatName { get; set; }
    }
    public class UpdateInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
