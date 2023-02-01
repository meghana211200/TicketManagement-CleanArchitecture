using System;
using Domain.Entites;

namespace Application.Interface;

public interface ILoginRepository
{

    public User CheckEmail(string email);

}

