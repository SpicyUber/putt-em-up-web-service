import React, { useState, useRef, useEffect, useContext } from "react";
import {
  Box,
  Stack,
  Paper,
  Typography,
  TextField,
  IconButton,
  Divider,
  Avatar,
  Toolbar,
  Button
} from "@mui/material";
import SendIcon from "@mui/icons-material/Send";
import { useParams } from "react-router-dom";
import UserContext, { type UserContextType } from "../hooks/UserContext";
import { useNavigate } from "react-router-dom";
import type { Message } from "../types/Message";
import type { Profile  } from "../types/Profile";
import type { Account } from "../types/Account";
 
import PrimarySearchAppBar from "../Components/PrimarySearchAppBar";
import type { DetailedMessageView } from "../types/DetailedMessageView";

 

export default function Recents() {
  const [messages, setMessages] = useState<DetailedMessageView[]>([]);
  
  const bottomRef = useRef<HTMLDivElement>(null);
    
   
    const userContext : UserContextType = useContext(UserContext);
     
   const [searchValue, setSearchValue] = useState("");

   

   

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);
  const navigate = useNavigate();
  useEffect(()=>{loadChat()},[])

async function loadChat(){
    if(userContext.user.playerID<0)navigate("/login/")
const url:string = `https://localhost:7120/api/messages/recent?playerID=${userContext.user.playerID}`;

const response = await fetch(url);
const result = await response.json() as Message[];
if(!response.ok){return;}
 
let s : Profile;
let r : Profile;
const messageArray:DetailedMessageView[] = [];
for(let i =0;i<result.length;i++){
let a1 = await loadAccount(result[i].fromPlayerID);
if(a1==undefined)return;
let p1 = await loadProfile(a1.username)
 ;
let a2 = await loadAccount(result[i].toPlayerID);
if(a2==undefined)return;
let p2 = await loadProfile(a2.username)
let detailedMessage:DetailedMessageView =  {message:result[i],from:p1 as Profile,to:p2 as Profile};
 messageArray.push(detailedMessage);
 
    }
    setMessages(messageArray);

}
  async function loadAccount(pid:BigInt){
          let url:string = `https://localhost:7120/api/accounts/${pid}`;
      
    try {
     
    const response : Response =  await fetch(url)

     let a:Account  = await response.json() as Account;
    return a;

    } catch (error) {

     console.log(error);
     console.log(url);
    }
     
   
     return undefined;
}
async function loadProfile(username:string){
          let url:string = `https://localhost:7120/api/profiles/${username}`;
      
    try {
     
    const response : Response =  await fetch(url)

     let p:Profile  = await response.json() as Profile;
    return p;

    } catch (error) {

     console.log(error);
     console.log(url);
    }
     
   
     return undefined;
}
function GoToChat(m:Message){
if(m.fromPlayerID==userContext.user.playerID){navigate("/chats/"+m.toPlayerID); return;}
if(m.toPlayerID==userContext.user.playerID){navigate("/chats/"+m.fromPlayerID);return;}

}

 

  return (
    <><PrimarySearchAppBar setSearchValue={setSearchValue}></PrimarySearchAppBar>
    <Toolbar></Toolbar>
     
    <Box sx={{height: "calc(100vh - 64px)",minWidth:"35vw", display: "flex", flexDirection: "column", bgcolor:"#287dd1ff" }}>
       <Typography  
   variant={"h4"}
  sx={{
    color: '#F5F5F5',
    whiteSpace: 'wrap',        
    overflow: 'visible',          
    textOverflow: 'clip',    
      justifyContent:'left',
    paddingTop:'12px'                
  }}>{(messages.length>0)?"MESSAGE HISTORY":"NO RECENT MESSAGES"}</Typography>
      <Box sx={{ flex: 1, overflowY: "auto", p: 2 }}>
        <Stack spacing={1}>
          {messages.map((m:DetailedMessageView)=>{ return( <Button onClick={()=>GoToChat(m.message)} sx={{bgcolor:"rgb(37, 113, 189)"}}><Box     p={1} borderRadius={1}  >
      <Typography variant="caption" sx={{color:"#F5F5F5"}}>
       { "FROM: "+ m.from.displayName+  " -> TO:"+ m.to.displayName}
      </Typography>
      <Typography variant="body1" sx={{color:"#F5F5F5"}}>
        {m.message.content}
      </Typography>
       <Typography variant="caption" sx={{color:"#F5F5F5"}}>
       { m.message.sentTimestamp.slice(0,16).replace("T"," | ")}</Typography>
    </Box></Button>);})}
          
        </Stack>
      </Box>
      
    </Box></>
  );
}
