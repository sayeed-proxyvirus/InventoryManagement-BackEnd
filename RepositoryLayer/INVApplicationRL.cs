using InventoryManagement.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement.RepositoryLayer
{
    public class INVApplicationRL : IINVApplicationRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        int ConnectionTimeOut = 1800;
        public INVApplicationRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration["ConnectionStrings:SqlServerDBConnection"]);
            //_mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse { IsSuccess = false, Message = "Invalid credentials." };

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("usp_login", _sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);

                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (await _sqlDataReader.ReadAsync())
                        {
                            string storedHashedPassword = _sqlDataReader["Upassword"].ToString();

                            if (BCrypt.Net.BCrypt.Verify(request.Password, storedHashedPassword))
                            {
                                response.IsSuccess = true;
                                response.Message = "Login successful!";
                            }
                        }
                        else
                        {
                            response.Message = "User not found.";
                        }
                    }

                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception: " + ex.Message;
            }

            return response;
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse { IsSuccess = false, Message = "Registration failed." };

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                using (SqlCommand sqlCommand = new SqlCommand("usp_register", _sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    sqlCommand.Parameters.AddWithValue("@FullName", request.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);


                    await _sqlConnection.OpenAsync();
                    await sqlCommand.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();

                    response.IsSuccess = true;
                    response.Message = "Registration successful!";
                }
            }
            catch (SqlException ex) when (ex.Number == 50000) // RAISERROR number
            {
                response.Message = ex.Message; // e.g., "User already exists."
            }
            catch (Exception ex)
            {
                response.Message = "Exception: " + ex.Message;
            }

            return response;
        }



        public async Task<CreateInformationResponse> CreateInformation(IItemCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_additem";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.ItemCode);
                        sqlCommand.Parameters.AddWithValue("@ItemName", request.ItemName);
                        sqlCommand.Parameters.AddWithValue("@CatName", request.CatName);
                        sqlCommand.Parameters.AddWithValue("@SubCatname", request.SubCatname);
                        sqlCommand.Parameters.AddWithValue("@OpeningStock", request.OpeningStock);
                        sqlCommand.Parameters.AddWithValue("@CurrentStock", request.CurrentStock);
                        sqlCommand.Parameters.AddWithValue("@ReorderStock", request.ReorderStock);
                        sqlCommand.Parameters.AddWithValue("@MaxStock", request.MaxStock);
                        sqlCommand.Parameters.AddWithValue("@LastPurRate", request.LastPurRate);
                        sqlCommand.Parameters.AddWithValue("@LastPurDate", request.LastPurDate);
                        sqlCommand.Parameters.AddWithValue("@PrefAlt", request.PrefAlt);
                        sqlCommand.Parameters.AddWithValue("@Amount", request.Amount);
                        sqlCommand.Parameters.AddWithValue("@HScode", request.HScode);
                        sqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                        sqlCommand.Parameters.AddWithValue("@CreatedAT", request.CreatedAT);
                        sqlCommand.Parameters.AddWithValue("@ExpiryDate", request.ExpiryDate);
                        


                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(SupplierInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addSupplier";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.SupplierID);
                        sqlCommand.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        sqlCommand.Parameters.AddWithValue("@ContactNumber", request.ContactNumber);
                        sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                        sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                        



                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(ItemCatCreateInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addItemCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@CategoryID", request.CategoryID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);

                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(SubCatCreateInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addItemCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@SubCatID", request.SubCatID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);
                        sqlCommand.Parameters.AddWithValue("@SubCatName", request.SubCatName);

                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }



        public async Task<ReadInformationResponse> ItemReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.itemreadinformation = new List<ItemReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItems";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ItemReadInformation getResponse = new ItemReadInformation();
                                getResponse.ItemCode = _sqlDataReader["ItemCode"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ItemCode"]) : 0;
                                getResponse.ItemName = _sqlDataReader["ItemName"] != DBNull.Value ? _sqlDataReader["ItemName"].ToString() : string.Empty;
                                getResponse.HScode = _sqlDataReader["HScode"] != DBNull.Value ? _sqlDataReader["HScode"].ToString() : string.Empty;
                                getResponse.CatName = _sqlDataReader["CatName"] != DBNull.Value ? _sqlDataReader["CatName"].ToString() : string.Empty;
                                getResponse.SubCatName = _sqlDataReader["SubCatName"] != DBNull.Value ? _sqlDataReader["SubCatName"].ToString() : string.Empty;
                                getResponse.LastPurDate = _sqlDataReader["LastPurDate"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["LastPurDate"]));
                                getResponse.Unit = _sqlDataReader["Unit"] != DBNull.Value ? _sqlDataReader["Unit"].ToString() : string.Empty;
                                getResponse.PrefAlt = _sqlDataReader["PrefAlt"] != DBNull.Value ? _sqlDataReader["PrefAlt"].ToString() : string.Empty;
                                getResponse.OpeningStock = _sqlDataReader["OpeningStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["OpeningStock"]) : 0;
                                getResponse.CurrentStock = _sqlDataReader["CurrentStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["CurrentStock"]) : 0;
                                getResponse.ReorderStock = _sqlDataReader["ReorderStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ReorderStock"]) : 0;
                                getResponse.MaxStock = _sqlDataReader["OpeningStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["MaxStock"]) : 0;
                                getResponse.LastPurRate = _sqlDataReader["LastPurRate"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["LastPurRate"]) : 0;
                                getResponse.Amount = _sqlDataReader["Amount"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Amount"]) : 0;
                                getResponse.CreatedAT = _sqlDataReader["CreatedAT"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["CreatedAT"]));
                                getResponse.ExpiryDate = _sqlDataReader["ExpiryDate"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["ExpiryDate"]));
                                getResponse.IsActive = _sqlDataReader["IsActive"] != DBNull.Value? Convert.ToBoolean(_sqlDataReader["IsActive"]): true;
                                response.itemreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }



    }
}
