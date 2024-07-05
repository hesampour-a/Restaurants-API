using MediatR;

namespace U4.Application.User.Commands;

public class UpdateUserCommand : IRequest
{
    public DateOnly? BirthDate { get; set; }
    public string? Nationality { get; set; }
}
