 
import PrimarySearchAppBar from "../Components/PrimarySearchAppBar";
import { BottomNavigation, Box, CssBaseline, FormControlLabel, FormGroup, Pagination, Switch, Toolbar } from "@mui/material";
import { useEffect, useState } from "react";
import PlayerCard from "../Components/PlayerCard";
import {Stack,Divider} from "@mui/material";
import type { Profile } from "../types/Profile";
import { useParams } from "react-router-dom";


export function Leaderboard( ){

    const [cardCount, setCardCount] = useState(5);
    const [pageNumber, setNumber] = useState(1);
   
    const [searchValue, setSearchValue] = useState("");
    const [searchResults,setSearchResults] = useState<Profile[]>([]);
    const [sortDescending,setSortDescending] = useState<boolean>(true);
    useEffect(()=>{loadProfiles()},[])
    useEffect(()=>{loadProfiles()},[sortDescending,searchValue,pageNumber])
    async function loadProfiles(){
          let url:string = `https://localhost:7120/api/profiles/search?DescendingRanking=${sortDescending}&PageSize=${cardCount}&PageNumber=${pageNumber}`;
       if(searchValue.length>0)url=url+`&UsernameStartsWith=${searchValue}`;
    try {
     
    const response : Response =  await fetch(url)


    setSearchResults(await response.json() as Profile[]);

    } catch (error) {

     console.log(error);
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
    <Box sx={{ minHeight: '100vh'}}>
        {(searchResults.length>0)?
    <FormGroup>
  <FormControlLabel control={<Switch defaultChecked={sortDescending} color="info" onChange={toggleSorting}/>} label={sortDescending?"Descending MMR" :"Ascending MMR"}  sx={{color:"#287dd1"}}/> </FormGroup> 
   : <>Nothing to see here.</>}
    <Stack bgcolor={'#c5d4e9ff'} sx={{ ml: -1 }} spacing={2}>
    
    {searchResults.map(p => (
      <PlayerCard playerID={p.playerID} displayName={p.displayName} description={p.description} avatarFilePath={p.avatarFilePath} />
    ))}
    </Stack>
 
    </Box>
  
    <Box
  sx={{
    position: 'fixed',
    bottom: 0,
    left: 0,
    width: '100%',
    bgcolor: '#fff',  
    py: 1,
    display: 'flex',
    justifyContent: 'center',
    boxShadow: '0 -2px 5px rgba(0,0,0,0.1)',  
    zIndex: 1000,  
  }}
>
  <Pagination
    count={calculatePaginationDisplayCount()}
     
    onChange={(_, value) => setNumber(value)}
    color="primary"
  />
</Box>
    </>)
}