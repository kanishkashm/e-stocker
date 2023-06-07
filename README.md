Add user role public api endpoint
	(get) https://localhost:7000/api/v1/account/AddUserRoles

Register user
	(post) https://localhost:7000/api/v1/account/Register
	payload:
		{
		   "confirmPassword":"string",
		   "email":"string",
		   "password":"string",
		   "role":"string"
		}
		roles 1: Admin, 2: User, 3: Auditor

IdentityServe:
	https://localhost:7000/connect/token
	payload
		client_id:eStoker
		client_secret:eStokerSecret
		grant_type:password
		username:{username}
		password:{password}
	
API Project
	API documentation using swwagger - https://localhost:7001/swagger/index.html

	Create stock item(attached bearer tocken to header)
		(post) https://localhost:7001/api/v2/Stock
		payload:
		{
		  "name": "string",//stock name
		  "item": {
			"name": "string", //item name
			"price": 0
		  }
		}
		
	View stock item(attached bearer tocken to header)
		(get) https://localhost:7001/api/v2/Stock/{stockId}
		
	Edit stock item(attached bearer tocken to header)
		(put) https://localhost:7001/api/v2/Stock/{itemId}
		payload:
		{
		  "id": 0,
		  "name": "string",
		  "price": 0
		}
		
	Stock issuer(public api)(handle concurrency)
		(put) https://localhost:7001/api/v2/Stock/issueStock/{stockId}
		payload:
		{
		  "stockId": 0,
		  "takenBy": "string",
		  "rowVersion": "string"
		}
	