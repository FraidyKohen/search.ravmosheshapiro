import React, { useState, useEffect, useContext } from 'react';
import axios from 'axios';

const SearchContext = React.createContext();

const SearchContextComponent = ({ children }) => {
    const [searchResults, setSearchResults] = useState([]);
    const value = {
        searchResults,
        setSearchResults
    }
    return (
        <SearchContext.Provider value={value}>
            {children}
        </SearchContext.Provider>
    )
}

const useSearchContext = () => useContext(SearchContext);

export { useSearchContext, SearchContextComponent };

