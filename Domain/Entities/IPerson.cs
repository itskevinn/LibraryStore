﻿using System;

namespace Domain.Entities
{
  public interface IPerson
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age { get; }
  }
}