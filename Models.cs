namespace Summary.Rubika
{
    public class ReceiveUpdateFormInfo
    {
        public ReceiveUpdateModel Update { get; set; }
    }

    public class ReceiveUpdateModel
    {
        public string Type { get; set; }
        public string Chat_Id { get; set; }
        public ReceiveUpdateNewMessageModel New_Message { get; set; }
    }

    public class ReceiveUpdateNewMessageModel
    {
        public string Message_Id { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public bool Is_Edited { get; set; }
        public string Sender_Type { get; set; }
        public string Sender_Id { get; set; }
    }

    public enum Status
    {
        OK,
        INVALID_ACCESS,
        INVALID_INPUT
    }

    public abstract class BaseResponseInfo
    {
        public Status Status { get; set; }
    }

    public class EndpointResponseInfo : BaseResponseInfo
    { }

    public class SendMessageResponseInfo : BaseResponseInfo
    { }

    public class RequestSendFileResponseInfo : BaseResponseInfo
    {
        public RequestSendFileData Data { get; set; }
    }

    public class RequestSendFileData
    {
        public string Upload_Url { get; set; }
    }

    public class UploadFileResponseInfo : BaseResponseInfo
    {
        public UploadFileData Data { get; set; }
    }

    public class UploadFileData
    {
        public string File_Id { get; set; }
    }
}
