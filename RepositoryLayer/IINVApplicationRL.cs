

using InventoryManagement.CommonLayer.Models;

namespace InventoryManagement.RepositoryLayer
{
    public interface IINVApplicationRL
    {
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<RegisterResponse> Register(RegisterRequest request);


        public Task<CreateInformationResponse> CreateInformation(IItemCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(SupplierInfoCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(ItemCatCreateInfoCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(SubCatCreateInfoCreateInformationRequest request);



        public Task<DeleteInformationResponse> IItemDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> SupplierDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> ItemCatDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> SubCatDeleteInfo(DeleteInformationRequest request);




        public Task<ReadInformationResponse> ItemReadInformation();
        public Task<ReadInformationResponse> ICReadInformation();
        public Task<ReadInformationResponse> SupCReadInformation();
        public Task<ReadInformationResponse> ItemCatCReadInformation();
        public Task<ReadInformationResponse> SubCatCReadInformation();
        public Task<ReadInformationResponse> SubCatInformation();
        public Task<ReadInformationResponse> SupplierReadInformation();
        public Task<ReadInformationResponse> ItemCatInformation();



        public Task<UpdateInformationResponse> UpdateInformation(ItemUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(SupplierUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(ItemCatUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(SubCatUpdateInformationRequest request);



        public Task<SearchInformationByNameResponse> ItemSearchInformationByName(SearchInformationByNameRequest request);
        public Task<SearchInformationByNameResponse> SupplierSearchInformationByName(SearchInformationByNameRequest request);
        public Task<SearchInformationByNameResponse> ItemCatSearchInformationByName(SearchInformationByNameRequest request);
        public Task<SearchInformationByNameResponse> SubCatSearchInformationByCat(SearchInformationByNameRequest request);
    }
}
