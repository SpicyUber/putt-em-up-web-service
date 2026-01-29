import { Autocomplete, Avatar, Box, Button, FormControl, Stack, TextField } from "@mui/material";
import React, { useContext, useState } from "react"
import { useNavigate } from "react-router-dom";
import UserContext, { type UserContextType } from "../hooks/UserContext";
import type { Profile } from "../types/Profile";
import { AccountCircle } from "@mui/icons-material";
import type { AutocompleteOption } from "../types/AutocompleteOption";
type AccountEditorProps = {
  onChange?: () => void;
  p : Profile;
};
export function EditProfile(props:AccountEditorProps) {
    
    const navigate = useNavigate();
     const [name, setName] = useState<string>(props.p.displayName);
      const [description, setDescription] = useState<string>(props.p.description);
      const [avatar, setAvatar] = useState<string>(props.p.avatarFilePath);
    const userContext :UserContextType = useContext(UserContext);
    const profilePics :AutocompleteOption[] = [
        "default.png", "screaming.png"

    ]
    const handleSubmit = async () => {
  const newProfile = {
    displayName:name,
    description:description,
    avatarFilePath: avatar
  };

 const r:Response = await fetch(
    `https://localhost:7120/api/profiles/${userContext.user.username}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(newProfile)
    }
  );
  console.log(r);
  if(r.status==200){props.onChange?.(
     
    );}
};
    return (<>
    <FormControl >
        <Stack spacing={2}>
        <TextField id="displayName" label="Name" variant="outlined" defaultValue={name}  onChange={(e) => setName(e.target.value)} />
        <TextField id="description" label="Description" variant="outlined"defaultValue={description}   onChange={(e) => setDescription(e.target.value)}/>
        
         <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
        
        <Autocomplete
  disablePortal
  options={profilePics} defaultValue={"default.png"}
  sx={{ width: 300 }} onChange={(e,value)=>{if(value!=null)setAvatar(value)}}
  renderInput={(params) => <TextField {...params} label="Avatar" />}
/><Avatar src={"../src/assets/profile-pictures/"+avatar} sx={{ color: 'action.active', mr: 1, my: 0.5 }} />
      </Box>
      <Button onClick={handleSubmit}>SAVE CHANGES</Button>
      </Stack>
    </FormControl> 
   
    </>);}

