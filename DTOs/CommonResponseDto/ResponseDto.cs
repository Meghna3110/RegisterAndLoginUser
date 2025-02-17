namespace User_Login_and_Registration.DTOs.CommonResponseDto
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }  
        public string Message { get; set; }
        //error msg
    }
}
