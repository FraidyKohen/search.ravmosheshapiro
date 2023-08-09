import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import SearchResultCard from './SearchResultCard';
import { useSearchContext } from './SearchContext';

const SearchResultsOriginal = () => {
    const [test, setTest] = useState(false);
    const { searchResults } = useSearchContext();
    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [searchResultsPages, setSearchResultsPages] = useState([]);

    useEffect(() => {
        console.log('using effect');
        setTest(true);
        const divideSearchResults = () => {
            console.log('using effect');
            console.log('dividing search results function');
            const searchResultsDivided = [];
            for (var i = 0; i < searchResults.length; i += 9) {
                const page = searchResults.slice(i, i + 9);
                searchResultsDivided.push(page);
            }
            console.log(searchResultsDivided)
            setSearchResultsPages(searchResultsDivided);
        }
        divideSearchResults();
    }, []);


    //useEffect(() => {
    //    console.log('divided');
    //        setTotalPages(searchResults.length / 9);
    //}, []);

    const onNextClick = () => {
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
            console.log(currentPage);
        }
    }

    if (searchResultsPages.length > 0) {
        return (
            <>
                <div className='col-md-12 offset-md-.5 bg-light p-3 rounded shadow justify-content-center'>
                    <div className='container'>
                        <div>
                            <button onClick={onNextClick}>Next
                            </button>
                        </div>
                        <div className='row justify-content-center'>
                            {searchResultsPages[currentPage].map(s => <SearchResultCard key={s.id} shiurContent={s.shiurContent} title={s.titleCalc} parshah={s.parshahYearHebreInHebrew} volumeIssue={s.volumeIssue} date={s.dateJewishEnglishCombined} displayText={s.wordsToDisplay} />)}
                        </div>
                    </div>
                </div>
            </>
        );
    }
    else {
        return (
            <>
                <div className='col-md-12 offset-md-.5 bg-light p-3 rounded shadow justify-content-center'>
                    <div className='container'>
                        <div>
                            <button onClick={onNextClick}>Next
                            </button>
                        </div>
                        <div className='row justify-content-center'>
                            {searchResultsPages[currentPage].map(s => <SearchResultCard key={s.id} shiurContent={s.shiurContent} title={s.titleCalc} parshah={s.parshahYearHebreInHebrew} volumeIssue={s.volumeIssue} date={s.dateJewishEnglishCombined} displayText={s.wordsToDisplay} />)}
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

export default SearchResultsOriginal;