import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import SearchResultCard from './SearchResultCard'
import { useSearchContext } from './SearchContext';

const SearchResults = () => {
    const navigate = useNavigate();
    const { searchResults } = useSearchContext();
    const [itemNumbers, setItemNumbers] = useState([]);
    const [itemNames, setItemNames] = useState([]);
    const [purchaseAllPrice, setPurchaseAllPrice] = useState(0.00);
    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [searchResultsPages, setSearchResultsPages] = useState([]);

    useEffect(() => {
        const names = [];
        searchResults.map(sr => names.push(`${sr.titleCalc} (${sr.parshahEnglish} ${sr.year})`));
        setItemNames(names);
        const numbers = [];
        searchResults.map(sr => numbers.push(`${sr.version} ${sr.volume}${sr.issue}`))
        setItemNumbers(numbers);
        console.log(numbers);
        setPurchaseAllPrice(searchResults.length * 3.95);
        const divideSearchResults = () => {
            const searchResultsDivided = [];
            for (var i = 0; i < searchResults.length; i += 9) {
                const page = searchResults.slice(i, i + 9);
                searchResultsDivided.push(page);
            }
            setSearchResultsPages(searchResultsDivided);
            setTotalPages(searchResultsDivided.length);
        }
        divideSearchResults();
    }, []);

    const onNextClick = () => {
        setCurrentPage(currentPage + 1);
    }
    const onPreviousClick = () => {
        setCurrentPage(currentPage - 1);
    }

    const on2NextClick = () => {
        setCurrentPage(currentPage + 2);
    }
    const on2PreviousClick = () => {
        setCurrentPage(currentPage - 2);
    }

    const onBackClick = () => {
        navigate('/');
    }
    const submitTitle = 'Add Pamphlet to Cart - Buy All Search';

    if (searchResultsPages.length > 0) {
        console.log(searchResultsPages);
        return (
            <>
                <div className='col-md-12 offset-md-.5 bg-light p-3 rounded shadow justify-content-center'>
                        <div className='row justify-content-center'>
                        {searchResultsPages[currentPage].map(s => <SearchResultCard parshahEnglish={s.parshahEnglish} year={s.year} language={s.version} title={s.titleCalc} parshah={s.parshahYearHebreInHebrew} volume={s.volume} issue={s.issue} date={s.dateJewishEnglishCombined} displayText={s.wordsToDisplay} />)}
                        </div>
                    <div className='row justify-content-center' style={{margin: 40} }>
                            <div className='col-md-2'>
                                <button className='btn btn-outline-dark w-100' onClick={onBackClick}>Back</button>
                            </div>
                            <div className='col-md-2'>
                                <button className='btn btn-secondary w-100' disabled={currentPage === 0} onClick={onPreviousClick}>Previous</button>
                            </div>
                            <div className='col-md-4 row justify-content-center'>
                                <div className='col-md-1' style={{padding: 0} }>
                                    {currentPage < 2 ? <></> : <p className='btn' onClick={on2PreviousClick}>{currentPage - 1}</p>}
                                </div>
                            <div className='col-md-1' style={{ padding: 0 }}>
                                {currentPage < 1 ? <></> : <p className='btn' onClick={onPreviousClick} >{currentPage}</p>}
                            </div>
                            <div className='col-md-1' style={{ padding: 0 }}>
                                <p className='btn active' style={{ fontWeight: 'bold' }}>{currentPage + 1}</p>
                                </div>
                                <div className='col-md-1' style={{ padding: 0 }}>
                                    {currentPage > totalPages - 2 ? <></> : <p className='btn' onClick={onNextClick}>{currentPage + 2}</p>}
                                </div>
                                <div className='col-md-1' style={{ padding: 0 }}>
                                    {currentPage > totalPages - 3 ? <></> : <p className='btn' onClick={on2NextClick}>{currentPage + 3}</p>}
                                </div>
                            </div>
                            <div className='col-md-2'>
                                <button className='btn btn-secondary w-100' disabled={currentPage === totalPages-1} onClick={onNextClick}>Next</button>
                            </div>
                        <div className='col-md-2 '>
                            <form target="paypal" action="https://www.paypal.com/cgi-bin/webscr" method="post" className="form-inline p-0 mx-auto my-0">
                                <input type="hidden" name="cmd" value="_cart" />
                                <input type="hidden" name="add" value="1" />
                                <input type="hidden" name="bn" value="webassist.dreamweaver.4_0_1" />
                                <input type="hidden" name="business" value="zyapublications@gmail.com" />
                                <input type="hidden" name="item_name" value={itemNames} />
                                <input type="hidden" name="item_number" value={itemNumbers} />
                                <input type="hidden" name="amount" value={purchaseAllPrice} />
                                <input type="hidden" name="currency_code" value="USD" />
                                <input type="hidden" name="receiver_email" value="zyapublications@gmail.com" />
                                <input type="hidden" name="mrb" value="R-3WH47588B4505740X" />
                                <input type="hidden" name="pal" value="ANNSXSLJLYR2A" />
                                <input type="hidden" name="no_shipping" value="0" />
                                <input type="hidden" name="no_note" value="0" />
                                <input type="hidden" name="weight" value=".13" />
                                <input type="hidden" name="weight_unit" value="lbs" />
                                <input type="submit" value="Purchase All" name="submit" title={submitTitle} className='btn btn-outline-dark w-100'  />

                            </form>
                        </div>

                        </div>
                    </div>

            </>
        );
    }
    else {
        navigate('/')
        return (
            <>
            </>
        );
    }
}

export default SearchResults;