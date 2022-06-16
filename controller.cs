
// controller 


 public IActionResult Authentication([FromBody] Credential credentialDetails)
        {
            if (credentialDetails.Username == "admin" && credentialDetails.password == "password")
            {

                // claims 
                List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name , "admin"),
                        new Claim(ClaimTypes.Email , "shavendragoesoft@gmail.com"),

                        // autherization 
                        new Claim("Department","Hero"),
                        new Claim("startDate","01/23/2021"),


                    };

                var expeireAt = DateTime.UtcNow.AddMinutes(10); // after 10 minute token expire

                return Ok(new
                {
                    access_token = this.CreateToken(claims,expeireAt),
                    expires_at = expeireAt
                });

            }
            ModelState.AddModelError("unauthorized", "you are not authorized to access the endpoint");
            return Unauthorized(ModelState);
        }


// private method for create token 
 private string CreateToken(IEnumerable<Claim> claims, DateTime expireDate)
        {


            var securityKey = Encoding.ASCII.GetBytes(this._configuration.GetValue<string>("SecretKey"));

            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expireDate,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(securityKey),
                        SecurityAlgorithms.HmacSha256Signature
                ));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }