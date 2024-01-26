using System.Runtime.Serialization;

namespace NotesWithTags.API.Exceptions;

[Serializable]
public class ForbiddenException : HttpStatusCodeException
{
    private readonly int _httpStatus = 403;

    public ForbiddenException()
    {
        HttpResponseStatusCode = _httpStatus;
    }

    public ForbiddenException(string message)
        : base(message)
    {
        HttpResponseStatusCode = _httpStatus;
    }

    public ForbiddenException(string message, Exception innerException)
        : base(message)
    {
        HttpResponseStatusCode = _httpStatus;
    }

    public ForbiddenException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        HttpResponseStatusCode = _httpStatus;
    }
}