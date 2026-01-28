import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import {Badge, Box, Button, Container, Divider, Paper, Stack  } from '@mui/material';
 import Grid from '@mui/material/Grid';
import { Avatar, Typography } from '@mui/material';
import type { Profile } from '../types/Profile';
import next from "../assets/next-button.png"
import { WrapText } from '@mui/icons-material';

export default function PlayerCard(p:Profile) {





    return (

    <Stack   sx={{  backgroundColor: '#287dd1ff'  }} direction={'row'} spacing={0}  >
       
    <Card  variant={'outlined'}     sx={{zIndex:4, width: "120px", height: "120px", backgroundColor: '#1976d2' }}>
<CardContent   sx={{paddingRight:"10px",paddingLeft:"15px", color: '#ffffff'}} >
     

<Avatar  variant='square'  sx={{ width: 90, height: 90 }} alt={p.displayName} src={"../src/assets/profile-pictures/"+p.avatarFilePath} />

 
 

 
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
  </Box>
     <Divider   orientation="vertical" flexItem /> 
      
        <Button  sx={{zIndex:"10",paddingRight:"20px",paddingLeft:"20px", color:'#ffffff' ,bgcolor:'#287dd1ff', width:20, height:120}}> <Avatar src={next}/> </Button>
        
    </Stack>);
}