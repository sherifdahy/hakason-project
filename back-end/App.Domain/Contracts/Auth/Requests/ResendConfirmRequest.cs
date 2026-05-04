using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Auth.Requests;

public record ResendConfirmRequest(string Email);
