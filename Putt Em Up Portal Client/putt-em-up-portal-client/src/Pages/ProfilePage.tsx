 
import PrimarySearchAppBar from "../Components/PrimarySearchAppBar";
import { Avatar, BottomNavigation, Box, Button, CssBaseline, FormControlLabel, FormGroup, Pagination, Switch, Toolbar, Typography } from "@mui/material";
import { useContext, useEffect, useState } from "react";
 
import {Stack,Divider} from "@mui/material";
import type { Profile } from "../types/Profile";
import { useNavigate, useParams } from "react-router-dom";
import type { Match } from "../types/Match";
import MatchCard from "../Components/MatchCard";
import type { Account } from "../types/Account";
import type { UserContextType } from "../hooks/UserContext";
import UserContext from "../hooks/UserContext";
import { EditProfile } from "./EditProfile";

export function ProfilePage(){

    const [cardCount, setCardCount] = useState(3);
    const [pageNumber, setNumber] = useState(1);
    const [searchValue, setSearchValue] = useState("");
    const [profileDetails, setProfileDetails] = useState<Profile>();
    const [searchResults,setSearchResults] = useState<Match[]>([]);
    const [sortDescending,setSortDescending] = useState<boolean>(true);
    const [isEditing,setIsEditing] = useState<boolean>(false);
    const [lastMatchesCount,setLastMatchesCount] = useState(100);
    const [winRate,setWinRate] = useState<number>(0);
    const [ranking,setRanking] = useState<number>(0);
    const { username } = useParams();
    const navigate = useNavigate();
    const userContext : UserContextType = useContext(UserContext);
    
    useEffect(()=>{loadPlayer()},[])
    
    useEffect(()=>{if(profileDetails!=null)loadMatches()},[profileDetails])
      useEffect(()=>{if(profileDetails!=null)loadRanking()},[profileDetails])
    useEffect(()=>{if(profileDetails!=null)loadMatches()},[pageNumber])
      useEffect(()=>{if(profileDetails!=null)loadStats()},[profileDetails])
async function loadRanking(){
let url:string = `https://localhost:7120/api/accounts/${profileDetails?.playerID}`;
const response : Response =  await fetch(url);
const account:Account =await response.json() as Account ;
setRanking(account.matchmakingRanking);
}
        async function loadStats(){
let url:string = `https://localhost:7120/api/matches?PlayerID=${profileDetails?.playerID}&StartDate=${new Date().toISOString()}&Mode=BeforeIncludingDate&PageSize=100&PageNumber=1`;
console.log(url);
const response : Response =  await fetch(url);
const matches:Match[] =await response.json() as Match[] ;
let counter:number=0;      
for(let i=0;i<matches.length;i++){
  if((matches[i].matchPerformances[0].wonMatch==true && matches[i].matchPerformances[0].player.playerID==profileDetails?.playerID ) || (matches[i].matchPerformances[1].wonMatch==true && matches[i].matchPerformances[1].player.playerID==profileDetails?.playerID ))counter++;
 
}console.log(counter);setWinRate(counter);setLastMatchesCount(matches.length);
        }
    async function loadPlayer(){
          let url:string = `https://localhost:7120/api/profiles/${username}`;
        
    try {
     
    const response : Response =  await fetch(url)

 let p:Profile = await response.json() as Profile;
    setProfileDetails(p);

    } catch (error) {

     console.log(error);
     console.log(url);
    }
    console.log(url);
     
}
 async function loadMatches(){
  console.log(profileDetails);
          let url:string = `https://localhost:7120/api/matches?PlayerID=${profileDetails?.playerID}&StartDate=${new Date().toISOString()}&Mode=BeforeIncludingDate&PageSize=${cardCount}&PageNumber=${pageNumber}`;
        
    try {
     
    const response : Response =  await fetch(url)


    setSearchResults(await response.json() as Match[]);

    } catch (error) {

     console.log(profileDetails);
     console.log(url);
    }
    console.log(url);
}
 function toggleSorting(){
setSortDescending(!sortDescending)

 }
 function calculatePaginationDisplayCount():number{
    let n: number = pageNumber;
    if(searchResults.length>0)
    return n+1;
else return n;

    
 }
    return(
    <>
    <PrimarySearchAppBar setSearchValue={setSearchValue}/>
    <Toolbar></Toolbar>
    <Typography  
   variant={"h4"}
  sx={{
    color: 'rgb(219, 219, 219)',
    whiteSpace: 'wrap',        
    overflow: 'visible',          
    textOverflow: 'clip',    
      justifyContent:'left',
    paddingTop:'12px'                
  }}>{"PROFILE"}</Typography> 
  <Stack bgcolor={"#287dd1ff"}   color={'#efefef'} minWidth={"40vw"}sx={{ ml: -1, padding:"15px" }}   spacing={'1vw'}>
    <Stack bgcolor={"rgb(35, 110, 185)"}   direction="row" sx={{   padding:"15px" }}  ><Avatar sx={{width:"8vw",height:'fit-content'}} alt={profileDetails?.displayName} src={"../src/assets/profile-pictures/"+profileDetails?.avatarFilePath} />
      
    <Typography  variant="h5">{profileDetails?.displayName}</Typography>
    
    
 </Stack>
 <Typography variant="h6"  justifyContent={"left"}  align='left'>{"DESCRIPTION:"}</Typography>
 <Typography bgcolor={"rgb(35, 110, 185)"} variant="body1"  justifyContent={"left"}  align='left'>{profileDetails?.description}</Typography>

 </Stack>
 <Typography variant="h6"  justifyContent={"left"}  align='left'>{"//// "}</Typography>
 <Stack bgcolor={"#287dd1ff"}   color={'#efefef'} minWidth={"40vw"}sx={{ ml: -1, padding:"15px" }}   spacing={'1vw'}>
  <Typography variant="h6"  justifyContent={"left"}  align='left'>{"STATS:"}</Typography>
 <Typography bgcolor={"rgb(35, 110, 185)"} variant="body1"  justifyContent={"left"}  align='left'>{"Won "+winRate+" out of last "+lastMatchesCount+" matches."}</Typography>
 </Stack>
  <Typography variant="h6"  justifyContent={"left"}  align='left'>{"\\\\\\\\ "}</Typography>
 <Stack bgcolor={"#287dd1ff"}   color={'#efefef'} minWidth={"40vw"}sx={{ ml: -1, padding:"15px" }}   spacing={'1vw'}>
  <Typography variant="h6"  justifyContent={"left"}  align='left'>{"MMR:"}</Typography>
 <Typography bgcolor={"rgb(35, 110, 185)"} variant="body1"  justifyContent={"left"}  align='left'>{"This player's matchmaking ranking is "+ranking+"."}</Typography>
 </Stack>
 
 {(profileDetails?.playerID!=userContext.user.playerID || isEditing)? <></> : <Box   > <Button onClick={()=>{setIsEditing(true)}} >EDIT PROFILE</Button></Box>}
 {isEditing?<Stack spacing={2} >


  <Button onClick={()=>{setIsEditing(false)}} >STOP EDITING</Button>
  <EditProfile p={profileDetails as Profile} onChange={()=>{loadPlayer()}} ></EditProfile>
 </Stack> :<>
    <Box sx={{ minHeight: '100vh'}}>
        {(searchResults.length>0)?
      <Typography  
   variant={"h4"}
  sx={{
    color: 'rgb(219, 219, 219)',
    whiteSpace: 'wrap',        
    overflow: 'visible',          
    textOverflow: 'clip',    
      
    paddingTop:'12px'                
  }}>{"MATCH HISTORY"}</Typography> 
   : <Typography  
   variant={"h4"}
  sx={{
    color: 'rgb(219, 219, 219)',
    whiteSpace: 'wrap',        
    overflow: 'visible',          
    textOverflow: 'clip',    
      
    paddingTop:'12px'                
  }}>{"END OF HISTORY"}</Typography> }
    <Stack bgcolor={'rgba(197, 212, 233, 0)'} sx={{ ml: -1 }} spacing={2}>
    
    {searchResults.map(m => (
      <MatchCard match={m} pid={profileDetails?.playerID as BigInt}  /> 
   ))}
   <Pagination
    count={calculatePaginationDisplayCount()}
     
    onChange={(_, value) => setNumber(value)}
    color="primary"
  />
    </Stack>
 
    </Box>
  </>
  }
    </>)
}