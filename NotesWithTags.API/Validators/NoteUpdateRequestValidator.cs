using FluentValidation;
using NotesWithTags.API.Models;

namespace NotesWithTags.API.Validators;

public class NoteUpdateRequestValidator : AbstractValidator<NoteUpdateRequest>
{
    public NoteUpdateRequestValidator()
    {
        RuleFor(x => x.NoteText).NotEmpty();
    }
}