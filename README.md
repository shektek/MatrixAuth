# MatrixAuth
Proof of concept prototype for TPP education system

Uses the following:

https://github.com/Half-Shot/matrix-dotnet-sdk/blob/master/Matrix/Api/LoginApi.cs


## Running the project:

1) Clone the repository
git clone https://github.com/shektek/MatrixAuth.git

2) Open solution in VS2019

3) Run the Project.

4) Your web browser should open a page something like http://localhost:52915/  - add swagger to the end of the URL: http://localhost:52915/swagger
And a Swagger page should be displayed.

5) NOW YUO SEE 3 endpoints. /api/User is used for creating users. /api/User/{username} checks whether an individual user is online. /api/User/active will provide a list of active users.

6) Access the **/api/user/create** endpoind and create a new user, you should receive a result "User Created Successfully! :)"

7) Test Login with the Created User in the **/api/user** endpoint, with the following request
```
{
	"homeserver": "matrix.org"
	"username": "UserNameHere",
	"password": "PasswordHere"
}
```

## Other notes

If you have an SSL certificate, make sure to configure that in the project properties and add the appropriate attributes tag on the User controller. This does not have CORS enabled because we absolutely do not want anybody accessing this externally.