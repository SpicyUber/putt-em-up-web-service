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
  Toolbar
} from "@mui/material";
import SendIcon from "@mui/icons-material/Send";
import { useParams } from "react-router-dom";
import UserContext, { type UserContextType } from "../hooks/UserContext";
import { useNavigate } from "react-router-dom";
import type { Message } from "../types/Message";
import type { Profile  } from "../types/Profile";
import type { Account } from "../types/Account";
import * as signalR from "@microsoft/signalr";
import PrimarySearchAppBar from "../Components/PrimarySearchAppBar";

 

export default function Chat() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [text, setText] = useState("");
  const bottomRef = useRef<HTMLDivElement>(null);
    const { id } = useParams();
    const [me, setMe]= useState<Profile>();
    const [recipient, setRecipient]=useState<Profile>();
    const userContext : UserContextType = useContext(UserContext);
    const [connection, setConnection] = useState<signalR.HubConnection|null>(null);
    const [searchValue, setSearchValue] = useState("");
    

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7120/messageHub', { accessTokenFactory: () => {return userContext.user.token}
      })
      .withAutomaticReconnect()
      .build();
      console.log("TOKEN"+userContext.user.token);
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(() => {
          connection.invoke('GetConnectionId').then(id =>
            console.log('Connection ID:', id)
          );
          console.log('Connected!');
          connection.on('RefreshChat', () => {
            loadChat();
           
          });
        })
        .catch(e => console.log('Connection failed: ', e));
    }
  }, [connection]);

   

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);
  const navigate = useNavigate();
  useEffect(()=>{loadChat()},[])
async function loadChat(){
    if(userContext.user.playerID<0){
       
             navigate("/login/")
}
const url:string = `https://localhost:7120/api/chats?firstPlayerID=${userContext.user.playerID}&secondPlayerID=${id}`;
const response = await fetch(url,{
      headers: {
        "Authorization": `Bearer ${userContext.user.token}`,
      },
    });
const result = await response.json() as Message[];
if(response.ok){setMessages(result)};
let a1 = await loadAccount(userContext.user.playerID);
if(id==undefined) return;
let a2 = await loadAccount( BigInt(id));
if(a1!=undefined && a2!=undefined){
   let p1 = await loadProfile(a1.username);
   let p2 = await loadProfile(a2.username);
   if(p1!=undefined && p2!=undefined){
    setMe(p1);
    setRecipient(p2);}
    }

}
  async function loadAccount(pid:BigInt){
          let url:string = `https://localhost:7120/api/accounts/${pid}`;
      
    try {
     
    const response : Response =  await fetch(url,{
      headers: {
        "Authorization": `Bearer ${userContext.user.token}`,
      },
    })

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
     
    const response : Response =  await fetch(url,{
      headers: {
        "Authorization": `Bearer ${userContext.user.token}`,
      },
    })

     let p:Profile  = await response.json() as Profile;
    return p;

    } catch (error) {

     console.log(error);
     console.log(url);
    }
     
   
     return undefined;
}

async function GoToProfile(pid:BigInt){
      let url:string = `https://localhost:7120/api/accounts/${pid}`;
        
    try {
     
    const response : Response =  await fetch(url,{
      headers: {
        "Authorization": `Bearer ${userContext.user.token}`,
      },
    })

 let p:Account = await response.json() as Account;
   let account = p;
 navigate(`/profiles/${account.username}`);
    } catch (error) {

     console.log(error);
     console.log(url);
    }
     
  
 }

function IsMine(m:Message):boolean{

return (m.fromPlayerID==userContext.user.playerID);
}

function WhoSentIt(m:Message):Profile|undefined{
    return (IsMine(m))? me : recipient;
}
   const handleSend = async () => {
    if (!text.trim() || connection==null) return;

   await connection.invoke(
  "SendMessage",
  me?.playerID,
  recipient?.playerID,
  text
);

    setText("");
     
  };

  return (
    <><PrimarySearchAppBar setSearchValue={setSearchValue}></PrimarySearchAppBar>
    <Toolbar></Toolbar>
    <Box sx={{minWidth:"25vw",maxWidth:"300px", height: "100%", display: "flex", flexDirection: "column" }}>
      <Box sx={{ flex: 1, overflowY: "auto", p: 2 }}>
        <Stack spacing={1}>
          {messages.map((m) => (<>  
            <Typography color={(IsMine(m))? "primary":"warning"} variant="body2" align={(!IsMine(m))?"left":"right"} paddingRight={(!IsMine(m))?"0":"4px"} paddingLeft={(!IsMine(m))?"4px":"0"}>{(IsMine(m))?me?.displayName:recipient?.displayName}</Typography>
            <Box
               
              sx={{ display: "flex", justifyContent: IsMine(m) ? "flex-end" : "flex-start" }}
            >
              {(IsMine(m))?<></> :(
                <Avatar onClick={()=>GoToProfile((WhoSentIt(m) as Profile).playerID )} src={"data:image/png;base64, "+WhoSentIt(m)?.avatar} sx={{ mr: 2, alignSelf: "flex-start" }}>
                  
                </Avatar>
                
              )}
              
              <Paper
              sx={{
              p: 1.5,
              maxWidth: "70%",
              bgcolor: IsMine(m) ? "primary.main" : "warning.main",
              color: IsMine(m) ? "primary.contrastText" : "text.primary",
              display: "inline-block",
              wordBreak: "break-word",
              }}
            >
  <Typography variant="body2" sx={{ whiteSpace: "pre-wrap" }}>
    {m.content}
  </Typography>
</Paper>

              {(!IsMine(m))?<></> :(
                <Avatar onClick={()=>GoToProfile((WhoSentIt(m) as Profile).playerID )} src={"data:image/png;base64, "+WhoSentIt(m)?.avatar} sx={{ ml: 2, alignSelf: "flex-end" }}>
                  
                </Avatar>
                
              )} 
           
            </Box><Typography color={(IsMine(m))? "disabled":"disabled"} variant="body2" align={(!IsMine(m))?"left":"right"} paddingRight={(!IsMine(m))?"0":"4px"} paddingLeft={(!IsMine(m))?"4px":"0"}>{m.sentTimestamp.slice(0, 16).replace("T"," | ")}</Typography></>
          ))}
          <div ref={bottomRef} />
        </Stack>
      </Box>
      <Divider />
      <Box sx={{ p: 1, display: "flex", gap: 1 }}>
        <TextField
          fullWidth
          multiline
          maxRows={4}
          placeholder="Type a message…"
          value={text}
          onChange={(e) => setText(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === "Enter" && !e.shiftKey) {
              e.preventDefault();
              handleSend();
            }
          }}
        />
        <IconButton color="primary" onClick={handleSend} disabled={!text.trim()}>
          <SendIcon />
        </IconButton>
      </Box>
    </Box></>
  );
}
