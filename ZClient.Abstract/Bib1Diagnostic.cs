//feature
namespace ZClient.Abstract
{
	public enum Bib1Diagnostic
	{
		PermanentSystemError = 1,
		TemporarySystemError = 2,
		UnsupportedSearch = 3,
		TermsOnlyIncludesExclusionOrStopWords = 4,
		TooManyArgumentWords = 5,
		TooManyBooleanOperators = 6,
		TooManyTruncatedWords = 7,
		TooManyIncompleteSubfields = 8,
		TruncatedWordsTooShort = 9,
		InvalidFormatForRecordNumberInSearchTerm = 10,
		TooManyCharactersInSearchStatement = 11,
		TooManyRecordsRetrieved = 12,
		PresentRequestOutOfRange = 13,
		SystemErrorInPresentingRecords = 14,
		RecordNotAuthorizedToBeSentIntersystem = 15,
		RecordExceedsPreferredMessageSize = 16,
		RecordExceedsExceptionalRecordSize = 17,
		ResultSetNotSupportedAsASearchTerm = 18,
		OnlySingleResultSetAsSearchTermSupported = 19,
		OnlyAndingOfASingleResultSetAsSearchTerm = 20,
		ResultSetExistsAndReplaceIndicatorOff = 21,
		ResultSetNamingNotSupported = 22,
		SpecifiedCombinationOfDatabasesNotSupported = 23,
		ElementSetNamesNotSupported = 24,
		SpecifiedElementSetNameNotValidForSpecifiedDatabase = 25,
		OnlyGenericFormOfElementSetNameSupported = 26,
		ResultSetNoLongerExistsUnilaterallyDeletedByTarget = 27,
		ResultSetIsInUse = 28,
		OneOfTheSpecifiedDatabasesIsLocked = 29,
		SpecifiedResultSetDoesNotExist = 30,
		ResourcesExhaustedNoResultsAvailable = 31,
		ResourcesExhaustedUnpredictablePartialResultsAvailable = 32,
		ResourcesExhaustedValidSubsetOfResultsAvailable = 33,
		UnspecifiedError = 100,
		AccessControlFailure = 101,
		ChallengeRequiredCouldNotBeIssuedOperationTerminated = 102,
		ChallengeRequiredCouldNotBeIssuedRecordNotIncluded = 103,
		ChallengeFailedRecordNotIncluded = 104,
		TerminatedAtOriginRequest = 105,
		NoAbstractSyntaxesAgreedToForThisRecord = 106,
		QueryTypeNotSupported = 107,
		MalformedQuery = 108,
		DatabaseUnavailable = 109,
		OperatorUnsupported = 110,
		TooManyDatabasesSpecified = 111,
		TooManyResultSetsCreated = 112,
		UnsupportedAttributeType = 113,
		UnsupportedUseAttribute = 114,
		UnsupportedTermValueForUseAttribute = 115,
		UseAttributeRequiredButNotSupplied = 116,
		UnsupportedRelationAttribute = 117,
		UnsupportedStructureAttribute = 118,
		UnsupportedPositionAttribute = 119,
		UnsupportedTruncationAttribute = 120,
		UnsupportedAttributeSet = 121,
		UnsupportedCompletenessAttribute = 122,
		UnsupportedAttributeCombination = 123,
		UnsupportedCodedValueForTerm = 124,
		MalformedSearchTerm = 125,
		IllegalTermValueForAttribute = 126,
		UnparsableFormatForUnNormalizedValue = 127,
		IllegalResultSetName = 128,
		ProximitySearchOfSetsNotSupported = 129,
		IllegalResultSetInProximitySearch = 130,
		UnsupportedProximityRelation = 131,
		UnsupportedProximityUnitCode = 132,
		ProximityNotSupportedWithThisAttributeCombinationAttribute = 201,
		UnsupportedDistanceForProximity = 202,
		OrderedFlagNotSupportedForProximity = 203,
		OnlyZeroStepSizeSupportedForScan = 205,
		SpecifiedStepSizeNotSupportedForScanStep = 206,
		CannotSortAccordingToSequence = 207,
		NoResultSetNameSuppliedOnSort = 208,
		GenericSortNotSupported = 209,
		DatabaseSpecificSortNotSupported = 210,
		TooManySortKeys = 211,
		DuplicateSortKeys = 212,
		UnsupportedMissingDataAction = 213,
		IllegalSortRelation = 214,
		IllegalCaseValue = 215,
		IllegalMissingDataAction = 216,
		SegmentationCannotGuaranteeRecordsWillFitInSpecifiedSegments = 217,
		EsPackageNameAlreadyInUse = 218,
		EsNoSuchPackageOnModifyDelete = 219,
		EsQuotaExceeded = 220,
		EsExtendedServiceTypeNotSupported = 221,
		EsPermissionDeniedOnEsIdNotAuthorized = 222,
		EsPermissionDeniedOnEsCannotModifyOrDelete = 223,
		EsImmediateExecutionFailed = 224,
		EsImmediateExecutionNotSupportedForThisService = 225,
		EsImmediateExecutionNotSupportedForTheseParameters = 226,
		NoDataAvailableInRequestedRecordSyntax = 227,
		ScanMalformedScan = 228,
		TermTypeNotSupported = 229,
		SortTooManyInputResults = 230,
		SortIncompatibleRecordFormats = 231,
		ScanTermListNotSupported = 232,
		ScanUnsupportedValueOfPositionInResponse = 233,
		TooManyIndexTermsProcessed = 234,
		DatabaseDoesNotExist = 235,
		AccessToSpecifiedDatabaseDenied = 236,
		SortIllegalSort = 237,
		RecordNotAvailableInRequestedSyntax = 238,
		RecordSyntaxNotSupported = 239,
		ScanResourcesExhaustedLookingForSatisfyingTerms = 240,
		ScanBeginningOrEndOfTermList = 241,
		SegmentationMaxSegmentSizeTooSmallToSegmentRecord = 242,
		PresentAdditionalRangesParameterNotSupported = 243,
		PresentCompSpecParameterNotSupported = 244,
		Type1QueryRestrictionOperandNotSupported = 245,
		Type1QueryComplexAttributevalueNotSupported = 246,
		Type1QueryAttributesetAsPartOfAttributeelementNotSupported = 247
	}
}