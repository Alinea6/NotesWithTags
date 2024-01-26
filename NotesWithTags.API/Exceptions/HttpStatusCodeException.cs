using System.Runtime.Serialization;

namespace NotesWithTags.API.Exceptions;

[Serializable]
public class HttpStatusCodeException :Exception
{
    public int HttpResponseStatusCode { get; set; }

    public HttpStatusCodeException()
        : base("")
    {
    }

    public HttpStatusCodeException(string message)
        : base(message)
    {
    }

    public HttpStatusCodeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public HttpStatusCodeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        info.AddValue("HttpResponseStatusCode", HttpResponseStatusCode);
        base.GetObjectData(info, context);
    }
}