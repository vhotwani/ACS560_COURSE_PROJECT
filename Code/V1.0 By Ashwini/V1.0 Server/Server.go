
//  Server Side Code V1.0
//  Created By Ashwini 

package main
import (
	"database/sql"
	_ "github.com/mattn/go-sqlite3"
)
// SQLite database driver


import (
 "net"
 "fmt"
 "bufio"
 "strings" 
 "log"
 "time"
)

// defining public variable 
var db *sql.DB


// It will  store obj value of userlogin table
type userlogin struct {
userid int
user_name string
user_type string
u_password string
createdBy  string
UpdatedBy string
Modifieddate string

}
 
// It will store obj value of userdetails table
type userdetails struct {
userid int
user_Full_name string
gender string
Location string
Contact  string
SecurityQus string
SecurityAns string
createdBy  string
UpdatedBy string
Modifieddate time.Time
 
}

// For Multiple client handling
func goclienthandle(conn net.Conn) {
 
var x string
//var y bool = false
var username string
var usertype string
var pass string
var P userlogin
var PD userdetails
var data_received string=""

//Database Connection 	
db, err := sql.Open("sqlite3", "/home/ashwini/work/DB1")
if err != nil { panic(err) }
if db == nil { panic("db nil") }
	
fmt.Println("Database Connection Established...")
	
	
// data receive from cient
		
message, _ := bufio.NewReader(conn).ReadString('\n')
data_received=string(message)
data_received=strings.TrimSpace(data_received)
fmt.Print(" Register data Received:", data_received)
result := strings.Split(data_received, "|") // each elements from client data are seperated by | identifier.
x=string(result[0]) // result[0] will give the which operation event have send the data
x=strings.TrimSpace(x) // will remove spaces
		
switch x {
		
		
		case "Login": 
						// this block is designed for login operation
						
						fmt.Println("Operation Received:", result[0])
						fmt.Println("Username Received:", result[1])
						username=string(result[1])
						username=strings.TrimSpace(username)
						//fmt.Println("Pass Received:", result[2])
						pass=string(result[2])
						pass=strings.TrimSpace(pass)
						
						//getting information from DB for received user string
						rows, err := db.Query("select userid, user_name,user_type,u_password,createdBy,UpdatedBy, Modifieddate from userlogin where user_name = ?",username )
						if err != nil {
										log.Fatal(err)
									  }
										defer rows.Close()
						for rows.Next() {
											err := rows.Scan(&P.userid, &P.user_name,&P.user_type,&P.u_password,&P.createdBy,&P.UpdatedBy, &P.Modifieddate)
											if err != nil {
															log.Fatal(err)
														  }
											fmt.Print(P.userid)
										}
										
						if(username == P.user_name && pass == P.u_password) {
																				fmt.Println("Success")
																				conn.Write([]byte(P.user_type + "\n"))
																			} else {
																						fmt.Println("Failed")
																						conn.Write([]byte("Failed" + "\n"))
																					}

					  
		case "Register", "SMRegister": 
		
		
						// this block is designed for Register operation
						
		
						// determining usertype
						
						if x=="Register" {
						usertype="enduser"
						} else {
						usertype="secmgr"
						}
						P.user_name=result[1]
						
						// checking whether entered username from new user is already exist or not
						rows, err := db.Query("select userid, user_name from userlogin where user_name = ?",P.user_name )
						i:=0
						if err != nil {
										log.Fatal(err)
									  }
										defer rows.Close()
						for rows.Next() {
											err := rows.Scan(&P.userid, &P.user_name)
											if err != nil {
															log.Fatal(err)
														  }
														  i++
											fmt.Print(P.userid)
										}
						if i!=0 {
						conn.Write([]byte("User already present" + "\n"))
						} else {
						
						
						// P is object of Userlogin type										
						P.user_name=result[1]
						P.user_type=usertype
						P.u_password=result[2]
						P.createdBy=result[1]
						P.UpdatedBy=result[1]
						
						
						// Inserting data in Userlogin Table
						
						DCL, err := db.Exec("Begin Transaction;")
						resultset, err := db.Exec("Insert into userLogin (user_name, user_type, u_password, createdBy, UpdatedBy, Modifieddate) values($1,$2,$3,$4,$5, datetime('now'))",P.user_name,P.user_type,P.u_password,P.createdBy,P.UpdatedBy)
						if err != nil {
										log.Fatal(err)
									  }
										//defer resultset.Close()
								
										fmt.Println(" ID insert : ",resultset)
						DCL, err = db.Exec("commit;")				
						
						
						//getting user_id for newly inserted user
						rows, err := db.Query("select userid, user_name from userlogin where user_name = ?",P.user_name )
						
						if err != nil {
										log.Fatal(err)
									  }
										defer rows.Close()
						for rows.Next() {
											err := rows.Scan(&P.userid, &P.user_name)
											if err != nil {
															log.Fatal(err)
														  }
														  
											fmt.Print(P.userid)
										}
						// PD is object of UserDetails type	
						PD.userid=P.userid
						PD.user_Full_name = result[3]
						PD.gender = result[4]
						PD.Location = result[5]
						PD.Contact = result[6]
						PD.SecurityQus = result[7]
						PD.SecurityAns = result[8]
						PD.createdBy  = result[1]
						PD.UpdatedBy =result[1]
						
						// Inserting data in UserDetails table
						DCL, err = db.Exec("Begin Transaction;")
						resultset2, err := db.Exec("Insert into userDetails (userid , user_Full_name , gender, Location, Contact , SecurityQus, SecurityAns ,createdBy, UpdatedBy, Modifieddate) values($1,$2,$3,$4,$5,$6,$7,$8, $9, datetime('now'))",PD.userid , PD.user_Full_name , PD.gender, PD.Location, PD.Contact , PD.SecurityQus, PD.SecurityAns ,PD.createdBy, PD.UpdatedBy)
						if err != nil {
										log.Fatal(err)
									  }
										//defer resultset.Close()
										fmt.Println(" ID insert : ",resultset2)
						DCL, err = db.Exec("commit;")	
						
						fmt.Println(" DCL : ",DCL)
						conn.Write([]byte("Success" + "\n"))
						
						}

						
					
						
		case "Profile": // Sending profile data to user.			
						 
						message, _ = bufio.NewReader(conn).ReadString('\n')
						username=""
						username=string(message)
						username=strings.TrimSpace(username)
						rows, err := db.Query("select u.userid , user_Full_name , gender, Location, Contact , SecurityQus, SecurityAns  from userDetails u inner join userlogin l on u.userid=l.userid where l.user_name = ?",username )
						
						if err != nil {
										log.Fatal(err)
									  }
										defer rows.Close()
						for rows.Next() {
											err := rows.Scan(&PD.userid , &PD.user_Full_name , &PD.gender, &PD.Location, &PD.Contact , &PD.SecurityQus, &PD.SecurityAns)
											if err != nil {
															log.Fatal(err)
														  }
														  
											fmt.Print(PD.user_Full_name)
										}
						var profileData string
						profileData=username + "|" + string(PD.userid) + "|" + PD.user_Full_name + "|" + PD.gender + "|" + PD.Location + "|" + PD.Contact + "|" + PD.SecurityQus + "|" + PD.SecurityAns
						fmt.Print(profileData)
						conn.Write([]byte(profileData + "\n"))

						
		
		
		case "User_Home": 
		
		case "Admin_Home": 
		
		case "SecMgr_Home": 
		
		case "Forgot_Password": 
		
		
		default: fmt.Println("This is defailt case")
		         
		
		}
		
		defer db.Close()
 }
 


func main() {






	fmt.Println("Launching server... (type ctrl-c to close the server)")
     
	// listen on all interfaces
	ln, _ := net.Listen("tcp", ":12000")

	// run loop forever (or until ctrl-c)
	for {
		// accept connection on port
		conn, _ := ln.Accept()
		
		go goclienthandle(conn);
	
		
		
	}
	
}
