// service
builder.Services.AddAuthentication(options => {
    //  නමක් තියලා නැති එක තමයි default එක මෙන්න
    //  උදාහරණයක් විදියට අපි හරියට  කුකී එකට  දැම්ම වගේ   option වලට කලින් නමක් යෝජනා කරන්න ඕනේ

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
   
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,  //  validate කරන්න යතුරක් පාවිච්චි කරනවද නැද්ද  කියන එක
        IssuerSigningKey = new SymmetricSecurityKey( //  ඒ යතුර මොකද්ද ?
            Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey"))
            ),
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuer   = false,
        ClockSkew = TimeSpan.Zero
    };
});