import React from 'react';
import { Route, Routes } from 'react-router';
import Layout from './Layout';
import Home from './Home'
import SearchResults from './SearchResultsOriginal';
import SearchResultsBlank from './SearchResults';
import { SearchContextComponent } from './SearchContext';
import FileUpload from './FileUpload';



const App = () => {
    return (
        <SearchContextComponent>
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/searchResults' element={<SearchResultsBlank />} />
                    <Route exact path='/fileUpload' element={<FileUpload/> } />
                </Routes>
            </Layout>
        </SearchContextComponent>
    )
}


export default App;