# JWT

This set of JWT utilities is designed to simple work with JWT tokens. The usage of a key pair (using RSA) is optional.

## JwtEncoder

This class is used to create/encode JWTs using a `JwtDescriptor`

```csharp title="Sample implementation"

var jwtDescriptor = new JwtDescriptor()
{
    Subject = "wemogy app",
    Audience = "https://wemogy.cloud",
    Issuer = "https://identity.wemogy.cloud"
};

var jwt = JwtEncoder.Encode(jwtDescriptor);

```

## JwtDecoder

This class is used to decode JWTs to JSON string/object

```csharp title="Sample implementation"

var json = JwtDecoder.Decode(jwt);

var payload = JwtDecoder.Decode<SimpleJwtPayload>(jwt);

```
