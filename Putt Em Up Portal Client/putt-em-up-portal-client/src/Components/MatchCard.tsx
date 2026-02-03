import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import {Badge, Box, Button, Container, Divider, Paper, Stack  } from '@mui/material';
 import Grid from '@mui/material/Grid';
import { Avatar, Typography } from '@mui/material';
import type { Profile } from '../types/Profile';
import next from "../assets/next-button.png"
import { WrapText } from '@mui/icons-material';
import type { Match } from '../types/Match';
import useWindowDimensions from "../hooks/WindowDimension"
import type { MatchPerformance } from '../types/MatchPerformance';
 import type { MatchCardProps } from '../types/MatchCardProps';
import type { Account } from '../types/Account';
import { useNavigate } from 'react-router-dom';
 
export default function MatchCard(props:MatchCardProps) {
  
const { height, width } = useWindowDimensions();
const navigate = useNavigate();
async function GoToProfile(pid:BigInt){
      let url:string = `https://localhost:7120/api/accounts/${pid}`;
        
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
function WonMatch(mp:MatchPerformance[],pid:BigInt){

    if(mp==undefined || pid==undefined || mp.length<2)return true;
    if(mp[0].wonMatch && mp[0].player.playerID == pid)return true;
    if(mp[1].wonMatch && mp[1].player.playerID == pid)return true;
    return false;
}


    return (

    <Stack     sx={{  backgroundColor: WonMatch(props.match.matchPerformances,props.pid)? "#287dd1ff":"#FC440F"   }} direction={'row'} spacing={0}  >
       
    <Card  variant={'outlined'} onClick={()=>GoToProfile(props.match.matchPerformances[0].player.playerID)}    sx={{zIndex:4, width:(width<500)?"80px" : "120px", height:(width<500)?"80px" : "120px", paddingBottom:(width<500)?"15px" :"0px", backgroundColor: WonMatch(props.match.matchPerformances,props.pid)? "rgb(38, 117, 197)":"#f65a3b"  }}>
    
<CardContent   sx={{paddingRight:(width<500)?"0px" :"10px",paddingLeft:(width<500)?"7.5px" :"15px" ,color: '#ffffff'}} >
     

<Avatar  variant='square'  sx={{ width: (width<500)?65 :90, height:(width<500)?65 :90 }} alt={props.match.matchPerformances[0].player.displayName} src={"data:image/png;base64, " +props.match.matchPerformances[0].player.avatar} />

 
 

 
</CardContent>
 

    </Card>

   <Box sx={{ flex: 1, px: 2, display: 'flex', flexDirection: 'column', justifyContent: 'center' }}> 
   <Box sx={{width:(width<500)?'15vw':'30vw', flex: 1, px: 2, display: 'flex', flexDirection: 'row', justifyContent: 'center' }}>
   {(width<500)?<></> : <Typography align='left'
   variant={(width<500)?"body1":"h6"}
  sx={{
    color: '#edededff',
    whiteSpace: 'nowrap',        
    overflow: 'hidden',          
    textOverflow: 'ellipsis',    
    width:'30vw',  
    paddingTop:'12px'                
  }}
>
  {props.match.matchPerformances[0].player.displayName}
</Typography>}
 <Typography align='center'
   variant={(width<500)?"caption":"h6"}
  sx={{
    color: '#edededff',
    whiteSpace: 'wrap', 
    textWrapMode:(width<500)?'nowrap':'wrap',       
    overflow: (width<500)?'visible':'hidden',          
    textOverflow: 'ellipsis',    
    width:'30vw',  
    paddingLeft:(width<500)?"15px":"0",
    paddingTop:'12px'                
  }}
>
    {(width<500)?("    "+props.match.matchPerformances[0].player.displayName.slice(0,1)+":"+props.match.matchPerformances[0].finalScore +'pts. vs '+props.match.matchPerformances[1].player.displayName.slice(0,1)+":" +props.match.matchPerformances[1].finalScore+"pts."):(props.match.matchPerformances[0].finalScore +' : '+ props.match.matchPerformances[1].finalScore)} 
</Typography>
 {(width<500)?<></> :  <Typography align='right'
   variant={(width<500)?"caption":"h6"}
  sx={{
    color: '#edededff',
    whiteSpace: 'nowrap',        
    overflow: 'hidden',          
    textOverflow: 'ellipsis',    
    width:'30vw',   
     paddingTop:'12px'                 
  }}
>
  {props.match.matchPerformances[1].player.displayName}
</Typography>}
 

  </Box>
 {(width<500)?<Box sx={{  flex: 1, px: 2, display: 'flex', flexDirection: 'row', justifyContent: 'center' }}>
     <Typography align='center'
  variant="caption"
  sx={{
    color: WonMatch(props.match.matchPerformances,props.pid)?'#105396':"#962a0c" ,
    whiteSpace: 'nowrap',
    fontSize: "12px",        
    overflow: 'hidden',          
    textOverflow: 'ellipsis',    
       
     paddingTop:'12px'                 
  }}
>
  {props.match.startDate.replace("T"," | ").replace("-","/").replace("-","/").substring(0,18)}
</Typography>
  </Box>:<Box sx={{width:'30vw', flex: 1, px: 2, display: 'flex', flexDirection: 'row', justifyContent: 'center' }}>
     <Typography align='center'
  variant="h6"
  sx={{
    color: WonMatch(props.match.matchPerformances,props.pid)?'#105396':"#962a0c" ,
    whiteSpace: 'nowrap',        
    overflow: 'hidden',          
    textOverflow: 'ellipsis',    
    width:'30vw',   
     paddingTop:'12px'                 
  }}
>
  {props.match.startDate.replace("T"," | ").replace("-","/").replace("-","/").substring(0,18)}
</Typography>
  </Box>}
  </Box>
  <Divider/>   
 <Card  variant={'outlined'}  onClick={()=>{console.log("click"+props.match.matchPerformances[1].player.displayName);GoToProfile(props.match.matchPerformances[1].player.playerID)}}     sx={{zIndex:4, width:(width<500)?"80px" : "120px",paddingBottom:(width<500)?"15px" :"0px", height:(width<500)?"80px" : "120px", backgroundColor: WonMatch(props.match.matchPerformances,props.pid)? "rgb(38, 117, 197)":"#FC440F" }}>
<CardContent  sx={{paddingRight:(width<500)?"0px" :"10px",paddingLeft:(width<500)?"7.5px" :"15px", color: '#ffffff'}} >
     

<Avatar  variant='square'  sx={{ width: (width<500)?65 :90, height:(width<500)?65 :90 }} alt={props.match.matchPerformances[1].player.displayName} src={"data:image/png;base64, " +props.match.matchPerformances[1].player.avatar} />

 
 

 
</CardContent>
 

    </Card>
    </Stack>);
}