import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import {Badge, Box, Button, Container, Divider, Paper, Stack  } from '@mui/material';
 import Grid from '@mui/material/Grid';
import { Avatar, Typography } from '@mui/material';
import type { Profile } from '../types/Profile';
import next from "../assets/next-button.png"
import { WrapText } from '@mui/icons-material';
import { useNavigate } from "react-router-dom"; 
import { useEffect, useState } from 'react';
import type { Account } from '../types/Account';



export default function PlayerCard(p:Profile) {
  const navigate = useNavigate();
 const [profile, setProfile] = useState<Profile>(p);
 const [ranking, setRanking] = useState<number>(0);
  
 useEffect(()=>{if(profile!=null)loadRanking();setProfile(p);},[p.playerID]);

 async function loadRanking(){
let url:string = `https://localhost:7120/api/accounts/${p?.playerID}`;
const response : Response =  await fetch(url);
const account:Account =await response.json() as Account ;
setRanking(account.matchmakingRanking);
}
async function GoToProfile(){
      let url:string = `https://localhost:7120/api/accounts/${profile?.playerID}`;
        
    try {
     
    const response : Response =  await fetch(url)

 let p:Account = await response.json() as Account;
   let account = p;
 navigate(`/profiles/${account.username}`);
    } catch (error) {

     console.log(error);
     console.log(url);
    }
     
  
 }


    return (

    <Stack   sx={{  backgroundColor: '#287dd1ff'  }} direction={'row'} spacing={0}  >
       
    <Card  variant={'outlined'}     sx={{zIndex:4, width: "120px", height: "120px", backgroundColor: '#1976d2' }}>
<CardContent   sx={{paddingRight:"10px",paddingLeft:"15px", color: '#ffffff'}} >
     

<Avatar  variant='square'  sx={{ width: 90, height: 90 }} alt={p.displayName} src={"data:image/png;base64, " +p.avatar} />

 
 

 
</CardContent>
 

    </Card>

   
   <Box sx={{width:'30vw', flex: 1, px: 2, display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
    <Typography align='left'
  variant="h5"
  sx={{
    color: '#edededff',
    whiteSpace: 'nowrap',        
    overflow: 'hidden',          
    textOverflow: 'ellipsis',    
    width:'30vw',                  
  }}
>
  {p.displayName}
</Typography>

<Typography align='left'
  variant="subtitle1"
  sx={{
    color: '#eeeeeeff',
    whiteSpace: 'nowrap',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    width:'30vw',
  }}
>
  
  {p.description}
</Typography>
<Typography align='left'
  variant="subtitle1"
  sx={{
    color:'#105396',
    whiteSpace: 'nowrap',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    width:'30vw',
  }}
>
  
  {ranking}
</Typography>
  </Box>
     <Divider   orientation="vertical" flexItem /> 
      
        <Button onClick={GoToProfile}  sx={{zIndex:"10",paddingRight:"20px",paddingLeft:"20px", color:'#ffffff' ,bgcolor:'#287dd1ff', width:20, height:120}}> <Avatar src={next}/> </Button>
        
    </Stack>);
}