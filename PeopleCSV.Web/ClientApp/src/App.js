import React from "react";
import { Route } from 'react-router-dom';
import Layout from "./Layout";
import Home from "./Pages/Home";
import Generate from './Pages/Generate';
import Upload from './Pages/Upload';



const App = () => {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/upload' component={Upload} />
            <Route exact path='/generate' component={Generate} />

        </Layout>
    )

}
export default App;