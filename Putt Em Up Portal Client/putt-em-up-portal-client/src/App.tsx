import { createContext, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Login from './Pages/Login'
import './App.css'
import {GlobalStyles} from '@mui/material'
import UserProvider from './hooks/UserProvider'
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom'
import { Leaderboard } from './Pages/Leaderboard'
import PrimarySearchAppBar from './Components/PrimarySearchAppBar'
import { ProfilePage } from './Pages/ProfilePage'
import Chat from './Pages/Chat'
import Recents from './Pages/Recents'



function App() {

 


  return (
    <>
      <GlobalStyles
          styles={{
            body: { backgroundColor: "#f5f5f5" },
          }}
        />
        <BrowserRouter>
        <UserProvider>
          <Routes >
            <Route path={"/"} element = {<Navigate to="/login"></Navigate>}/>
            <Route path={"/login"} element={<Login/>}/>
            <Route path={"/leaderboard/" } element={<Leaderboard/>}></Route>
            <Route path={"/profiles/:username" } element={<ProfilePage/>}></Route>
            <Route path={"/chats/:id" } element={<Chat/>}></Route>
            <Route path={"/messages/recent" } element={<Recents/>}></Route>
          </Routes>
      
      </UserProvider>
      </BrowserRouter>
    </>
    
  )
}

 

export default App
