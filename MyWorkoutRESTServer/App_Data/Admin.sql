DELETE FROM USERS WHERE UserName='Admin';

INSERT INTO USERS(UserName, FirstName, LastName, Password, CurrentDay, CurrentWeek, Role) Values ('Admin', 'Admin', 
'Admin', 'Password', 1, 1, 'Admin');