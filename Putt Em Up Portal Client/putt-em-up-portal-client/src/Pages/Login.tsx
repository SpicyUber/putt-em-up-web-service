import React, { useContext, useState } from "react";
import TextField from "@mui/material/TextField"
import Typography from "@mui/material/Typography";
import  Stack from "@mui/material/Stack";
import  Button  from "@mui/material/Button";
import background from "/src/assets/putt-em-up-bg.png";
import Box from "@mui/material/Box";

import type { LoginError } from "../types/LoginError";
import type { LoginAnswer } from "../types/LoginAnswer";
import UserContext, { type UserContextType } from "../hooks/UserContext";
import { useNavigate } from "react-router-dom";






function Login() {
const navigate = useNavigate();
const userContext : UserContextType = useContext(UserContext);
const [username,setUsername] = useState("");
const [password,setPassword] = useState("");
const [error,setError] = useState("");
const [isRegistering,setIsRegistering] = useState(false);


function LoginButtonHandler(){

    AttemptLogin(username,password);
}



 async function AttemptLogin(username:string,password:string): Promise<void> {
    const url = isRegistering?"https://localhost:7120/api/register" :"https://localhost:7120/api/login"
  const response = await fetch(url, {
      method: 'POST',
      body: JSON.stringify({
        username,password
      }),
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
    });

    if(response.ok){ setError("");

 let data : LoginAnswer = await response.json() as LoginAnswer;
    userContext.updateUser(data);
      navigate("/leaderboard");
    }
    else{
         
        let data : LoginError = await response.json() as LoginError;
         let err:string = data.title+" ";
        
         for (const key in data.errors) {
    
        err+=data.errors[key].join(", ");
        }
        if(data.title==undefined)err=(isRegistering)?"Account with that username exists."
        :"Incorrect username and/or password.";
        
        setError(err);
    }

}

const handleUsernameChange = (event : any) => {
setUsername(event.target.value);
};

const handlePasswordChange = (event : any) => {
setPassword(event.target.value);

};

return (
   
 <Box>
    <Box component="img"
        src= "/src/assets/putt-em-up-bg.png"
        alt="Banner"
        sx={{
          width: "240px",
          height: "100%",
          objectFit: "cover",}}></Box>
    <Stack width={480} spacing={2}>
        <Typography variant="h2" component="h2">
        {(isRegistering)?"Register.":"Welcome."}  
            <Typography>Putt Em Up Portal</Typography>
        </Typography>

        <TextField onChange={handleUsernameChange} id="outlined-basic" label="Username" variant="outlined" />
        <TextField onChange={handlePasswordChange} id="outlined-basic" label="Password" variant="outlined" />
        <Button onClick={LoginButtonHandler} variant="contained">{(isRegistering)?"Create account":"Sign in"}</Button>
        <Button variant="text" onClick={()=>setIsRegistering(!isRegistering)}>{(isRegistering)?"Have account? Sign in instead!":"New here? Create an account!"}</Button>
        <Typography color="#F6AF3B" variant="subtitle1">{error}</Typography>
    </Stack>
</Box>

);}

export default Login;