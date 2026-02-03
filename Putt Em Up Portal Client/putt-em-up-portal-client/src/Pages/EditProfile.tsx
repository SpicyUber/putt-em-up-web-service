import { Autocomplete, Avatar, Box, Button, FormControl, Stack, TextField } from "@mui/material";
import React, { useContext, useState } from "react"
import { useNavigate } from "react-router-dom";
import UserContext, { type UserContextType } from "../hooks/UserContext";
import type { Profile } from "../types/Profile";
import { AccountCircle } from "@mui/icons-material";
import type { AutocompleteOption } from "../types/AutocompleteOption";
import { styled } from '@mui/material/styles';
 
import CloudUploadIcon from '@mui/icons-material/CloudUpload';

const VisuallyHiddenInput = styled('input')({
  clip: 'rect(0 0 0 0)',
  clipPath: 'inset(50%)',
  height: 1,
  overflow: 'hidden',
  position: 'absolute',
  bottom: 0,
  left: 0,
  whiteSpace: 'nowrap',
  width: 1,
});
type AccountEditorProps = {
  onChange?: () => void;
  p : Profile;
};
function blobToBase64(blob: Blob): Promise<string> {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onerror = () => reject(new Error("Failed to read blob"));
    reader.onload = () => {
      
      const dataUrl = reader.result as string;
      
      const base64 = dataUrl.split(",")[1];
      resolve(base64);
    };
    reader.readAsDataURL(blob);
  });
}
export function EditProfile(props:AccountEditorProps) {
    
    const navigate = useNavigate();
     const [name, setName] = useState<string>(props.p.displayName);
      const [description, setDescription] = useState<string>(props.p.description);
      const [avatar, setAvatar] = useState<string>(props.p.avatar);
    const userContext :UserContextType = useContext(UserContext);
    const profilePics :AutocompleteOption[] = [
        "default.png", "screaming.png"

    ]
    const handleSubmit = async () => {
  const newProfile = {
    displayName:name,
    description:description,
    avatarInBase64: avatar
  };
console.log(JSON.stringify(newProfile));
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
        <Box display="flex" flexDirection="row"  justifyContent={"flex-start"}
   px={2}   >
        <Button  sx={{marginLeft:"-16px", marginRight:"16px"}}
  component="label"
  role={undefined}
  variant="contained"
  size="medium"
  tabIndex={-1}
  startIcon={<CloudUploadIcon />}
>
  Upload avatar
  <VisuallyHiddenInput
    type="file"
    accept="image/png"
    onChange={ async (event) => {let a:string; if(event.target.files!=null && event.target.files.length>0 && event.target.files.item(0)!=null){a =await blobToBase64(await (event.target.files.item(0) as File)); setAvatar(a);}}}
    multiple
  />
</Button>
<Avatar src={"data:image/png;base64, " + avatar}></Avatar>
       </Box>
      <Button onClick={handleSubmit}>SAVE CHANGES</Button>
      </Stack>
    </FormControl> 
   
    </>);}

