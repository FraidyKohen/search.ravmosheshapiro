import React, { useEffect, useState } from "react";
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import SearchResults from "./SearchResultsOriginal";
import { useSearchContext } from "./SearchContext";
import * as Loader from "react-loader-spinner";



const Home = () => {
    const navigate = useNavigate();

    const [justLoaded, setJustLoaded] = useState(true);
    const { searchResults } = useSearchContext();
    const { setSearchResults } = useSearchContext();
    const [searchPhrase, setSearchPhrase] = useState('');
    const [maxSpace, setMaxSpace] = useState(5);
    const [loading, setLoading] = useState(false);
    const [noResults, setNoResults] = useState(false);
    const [searchedShiurim, setSearchedShiurim] = useState([]);
    const [searchKeyword, setSearchKeyword] = useState('');

    const onInputChange = e => {
        setSearchPhrase(e.target.value);
    }

    const onNumberChange = e => {
        setMaxSpace(e.target.value);
    }

    useEffect(() => {
        updateSearch();
        if (searchedShiurim.length > 0) {
            navigate('/searchResults');
        }
        else {
            if (!justLoaded) {
                setNoResults(true);
                setLoading(false);
            }
            else {
                setJustLoaded(false);
            }
        }
    }, [searchedShiurim]);


    const onSearchClick = () => {
        setLoading(true);
        const parameters = `${searchPhrase}*${maxSpace}`;
        const searchShiurim = async () => {
            const { data } = await axios.get(`/api/search/SearchShiurim/${parameters}`);
            console.log(data);
            setSearchedShiurim(data);
            return data;
        }
        const result = searchShiurim();
        getSearchKeyword();
    }

    const updateSearch = () => {
        getWordsToDisplay();
        setSearchResults(searchedShiurim);
    }

    const getSearchKeyword = () => {
        const words = searchPhrase.split(' ');
        for (var i = 0; i < words.length; i++) {
            if (words[i].length > 5) {
                setSearchKeyword(words[i]);
                return;
            }
        }
        for (var i = 0; i < words.length; i++) {
            if (words[i].length > 3) {
                setSearchKeyword(words[i]);
                return;
            }
        }
        setSearchKeyword(words[0]);
    }


    const getWordsToDisplay = () => {
        const copy = searchedShiurim;
        for (var i = 0; i < searchedShiurim.length; i++) {
            const content = searchedShiurim[i].shiurContent;
            let index = content.indexOf(searchPhrase);
            if (index < 0) {
                index = content.indexOf(searchKeyword);
            }
            const rawText = content.substring(index - 24, index + 130);
            const words = rawText.split(' ');
            let textToDisplay = '';
            for (var x = 1; x < words.length - 2; x++) {
                textToDisplay = textToDisplay + ' ' + words[x];
            }
            copy[i].wordsToDisplay = `${textToDisplay}`;
            if (searchedShiurim[i].volume < 10) {
                copy[i].volume = `0${searchedShiurim[i].volume}`;
            }
            if (searchedShiurim[i].issue < 10) {
                copy[i].issue = `0${searchedShiurim[i].issue}`;
            }
        }
        setSearchedShiurim(copy);
    }

    if (loading == true) {
        return (
            <>
                <div className='col-md-4 offset-4'>
                    <Loader.ThreeDots color="rgb(80, 80, 80)" height={400} width={400} timeout={100000} />
                </div>
            </>);
    }
    else {
        return (
            <>
                <div style={{ padding: 180 }}>
                    <div className='bg-light p-3 rounded shadow justify-content-center flex'>
                        <div className="text-center justify-content-center mb-4" style={{ alignItems: 'flex-end' }}>
                            <div >
                                <input onChange={onInputChange} value={searchPhrase} style={{fontSize: 30}} placeholder='Search for a word or phrase in English or Hebrew' className='form-control-lg form-control w-100' />
                            </div>
                            <button onClick={onSearchClick} disabled={searchPhrase === ''} style={{ backgroundColor: '#080464' }} className='btn btn-primary w-100'>Search</button>
                            <br />
                            {noResults ? <p>No search results</p> : <></>}

                        </div>
                        <div>
                            <div className="col-md-2 offset-5 form-outline me-3 text-center">
                                <br />
                                <br />
                                <br />
                                <div>
                                    <div className='col-md-6 offset-3'>
                                <input type='number' onChange={onNumberChange} value={maxSpace} className='form-control text-center' />
                                    </div>
                                        <label className='justify-text-center form-label'>Average distance between words</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer>
                    <div className='text-center'>
                        <p>לעילוי נשמת מרת שיינע בת יעקב ז"ל</p>
                    </div>
                </footer>
            </>
        );
    }
}

export default Home;